using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        private readonly Dictionary<string, object> _parameterReplacements;

        public IncDecExpressionVisitor(Dictionary<string, object> parameterReplacements)
        {
            _parameterReplacements = parameterReplacements;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            // First, visit the left and right nodes to apply parameter replacements
            var left = Visit(node.Left);
            var right = Visit(node.Right);

            // Handle increment: <variable> + 1
            if (node.NodeType == ExpressionType.Add &&
                right is ConstantExpression rightConst &&
                rightConst.Value.Equals(1))
            {
                return Expression.Increment(left);
            }

            // Handle decrement: <variable> - 1
            if (node.NodeType == ExpressionType.Subtract &&
                right is ConstantExpression rightConstSub &&
                rightConstSub.Value.Equals(1))
            {
                return Expression.Decrement(left);
            }

            // Return the rebuilt binary expression with replaced nodes
            return Expression.MakeBinary(node.NodeType, left, right);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            // Replace parameter with constant if a replacement is provided
            if (_parameterReplacements.TryGetValue(node.Name, out var replacementValue))
            {
                return Expression.Constant(replacementValue, node.Type);
            }

            return node; // Return the original node if no replacement is found
        }

        public Expression Transform(Expression expression)
        {
            // Visit the expression tree and replace parameters with constants
            return Visit(expression);
        }
    }
}