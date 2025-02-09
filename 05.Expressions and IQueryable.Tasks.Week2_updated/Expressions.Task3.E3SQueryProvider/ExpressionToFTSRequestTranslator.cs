using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);
            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }

            // Handle string methods
            if (node.Method.Name == "StartsWith")
            {
                Visit(node.Object); // The property being checked
                _resultStringBuilder.Append("(");
                Visit(node.Arguments[0]); // The constant value
                _resultStringBuilder.Append("*"); // Append wildcard for StartsWith
                _resultStringBuilder.Append(")");
            }
            else if (node.Method.Name == "EndsWith")
            {
                Visit(node.Object); // The property being checked
                _resultStringBuilder.Append("(");
                _resultStringBuilder.Append("*"); // Prepend wildcard for EndsWith
                Visit(node.Arguments[0]); // The constant value
                _resultStringBuilder.Append(")");
            }
            else if (node.Method.Name == "Contains")
            {
                Visit(node.Object); // The property being checked
                _resultStringBuilder.Append("(");
                _resultStringBuilder.Append("*"); // Prepend wildcard for Contains
                Visit(node.Arguments[0]); // The constant value
                _resultStringBuilder.Append("*"); // Append wildcard for Contains
                _resultStringBuilder.Append(")");
            }
            else if (node.Method.Name == "Equals")
            {
                // Handle Equals method
                Visit(node.Object); // The property being checked
                _resultStringBuilder.Append("(");
                Visit(node.Arguments[0]); // The constant value
                _resultStringBuilder.Append(")");
            }
            else
            {
                throw new NotSupportedException($"Method '{node.Method.Name}' is not supported.");
            }

            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Equal)
            {
                if (node.Left.NodeType == ExpressionType.MemberAccess && node.Right.NodeType == ExpressionType.Constant)
                {
                    Visit(node.Left);
                    _resultStringBuilder.Append("(");
                    Visit(node.Right);
                    _resultStringBuilder.Append(")");
                }
                else if (node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType == ExpressionType.MemberAccess)
                {
                    Visit(node.Right);
                    _resultStringBuilder.Append("(");
                    Visit(node.Left);
                    _resultStringBuilder.Append(")");
                }
                else
                {
                    throw new NotSupportedException($"Both operands must be a property or a constant: {node.NodeType}");
                }
            }
            else if (node.NodeType == ExpressionType.AndAlso)
            {
                _resultStringBuilder.Append("{ \"statements\": [");
                _resultStringBuilder.Append("{ \"query\":\"");
                Visit(node.Left);
                _resultStringBuilder.Append("\"},{ \"query\":\"");
                Visit(node.Right);
                _resultStringBuilder.Append("\"}");
                _resultStringBuilder.Append("] }");
            }
            else
            {
                throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append(node.Value);

            return node;
        }

        #endregion
    }
}