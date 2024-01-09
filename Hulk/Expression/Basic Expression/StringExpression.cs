using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class StringExpression : Expression
    {
        public Token StringToken;
        public override ExpressionType Type => ExpressionType.StringExpression;
        public StringExpression(Token stringtoken)
        {
            StringToken = stringtoken;
        }

    }
}
