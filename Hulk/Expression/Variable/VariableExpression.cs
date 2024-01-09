using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class VariableExpression:Expression
    {
        public string Name { get; }
       
        public override ExpressionType Type => ExpressionType.VariableExpression;
        public VariableExpression(string name)
        {
            Name = name;
        }
    }
}
