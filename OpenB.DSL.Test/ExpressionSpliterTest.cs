using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenB.DSL.Test
{
    [TestFixture]
    public class ExpressionSpliterTest
    {
        public object RegEx { get; private set; }

        [Test]
        public void DoSomething()
        {
            string expression = "((3 + (6 * (1+3) *2) + 1 / 3 + (2+2) * 6)) -1";
            GetChildExpression(expression);

        }

        private static string GetChildExpression(string expression)
        {
            int end = expression.IndexOf(')');

            if (end < 0)
            {
                return expression;
            }

            int start = expression.Substring(0, end - 1).LastIndexOf('(');

            string subString = expression.Substring(start + 1, (end - start) - 1);

            var result = Evaluate(subString);

           expression =  expression.Remove(start, subString.Length + 2);

           expression = expression.Insert(start, result);

           return GetChildExpression(expression);
        }

        private static string Evaluate(string subString)
        {
            return "1";
        }
    }
}
