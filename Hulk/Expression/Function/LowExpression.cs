using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class LogExpression : Expression
    {
        public Expression value1;
        public Expression value2;
        public LogExpression(Expression Value1, Expression Value2)
        {
            value1 = Value1;
            value2 = Value2;
        }
        public override ExpressionType Type => ExpressionType.LogExpression;
    }
   
}
