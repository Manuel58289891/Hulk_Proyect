using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class FunctionVariableExpression:Expression
    {
     public string Name { get; }
        public FunctionVariableExpression(string name)
        {
            Name = name;
        }
        public override ExpressionType Type => ExpressionType.FunctionVariableExpression;
    }
}
