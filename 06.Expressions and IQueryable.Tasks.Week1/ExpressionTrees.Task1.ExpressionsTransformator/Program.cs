﻿/*
 * Create a class based on ExpressionVisitor, which makes expression tree transformation:
 * 1. converts expressions like <variable> + 1 to increment operations, <variable> - 1 - into decrement operations.
 * 2. changes parameter values in a lambda expression to constants, taking the following as transformation parameters:
 *    - source expression;
 *    - dictionary: <parameter name: value for replacement>
 * The results could be printed in console or checked via Debugger using any Visualizer.
 */
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expression Visitor for increment/decrement.");
            Console.WriteLine();

            // Example expression: x => x + 1
            Expression<Func<int, int>> expression = x => x + 1;

            // Create a dictionary for parameter replacement
            Console.WriteLine("Enter value(only int) for increment/decrement.");
            var inputValue = Int32.Parse(Console.ReadLine());
            var replacements = new Dictionary<string, object>
            {
                { "x", inputValue } 
            };

            // Create and apply the visitor
            var visitor = new IncDecExpressionVisitor(replacements);
            var transformedBody = visitor.Transform(expression.Body);

            // Create a new lambda expression without parameters
            var transformedExpression = Expression.Lambda<Func<int>>(transformedBody);

            // Display the transformed expression
            Console.WriteLine("Transformed Expression: " + transformedExpression);

            // Compile the transformed expression into a delegate
            var compiledExpression = transformedExpression.Compile();

            // Execute the compiled expression and get the result
            int result = compiledExpression(); // No parameter needed as x is replaced

            // Output the result
            Console.WriteLine($"Result of the expression: {result}"); // Should output 6

            Console.ReadLine();
        }
    }
}
