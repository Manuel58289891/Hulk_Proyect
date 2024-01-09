
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
     class NumberExpression:Expression
     { 
        public readonly Token NumberToken;
        public override ExpressionType Type => ExpressionType.numberExpression;
        public NumberExpression(Token numbertoken)
        {
            NumberToken = numbertoken;
        }
    }
}
