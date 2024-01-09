using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class ParenthesisExpression:Expression
    {
        public Expression Expression;
        public override ExpressionType Type => ExpressionType.ParenthesisExpression;
        public ParenthesisExpression(Expression expression)
        {
            Expression = expression;
        }
    }
    
}
