using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class ConditionalExpression : Expression
    {
        public Expression IfExpression;
        public Expression Expression;
        public Expression ElseExpression;
        public override ExpressionType Type => ExpressionType.ConditionalExpression;
        public ConditionalExpression(Expression ifExpression, Expression expression, Expression elseExpression)
        {
            IfExpression = ifExpression;
            Expression = expression;
            ElseExpression = elseExpression;

        }


    }
}
