using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class ErrorExpression:Expression
    {
        public string Message { get; }
        public override ExpressionType Type => ExpressionType.errorExpression;
        public ErrorExpression(string message)
        {
            Message = message;
        }
    }
}
