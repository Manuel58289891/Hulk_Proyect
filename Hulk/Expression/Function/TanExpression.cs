using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class TanExpression : Expression
    {
        public Expression value;
        public TanExpression(Expression Value)
        {
            value = Value;
        }
        public override ExpressionType Type => ExpressionType.TanExpression;
    }
}
