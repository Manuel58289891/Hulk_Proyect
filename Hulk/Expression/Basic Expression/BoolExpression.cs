using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class BoolExpression : Expression
    {
        public Token BoolToken { get; }
        public override ExpressionType Type => ExpressionType.BoolExrpession;
        public BoolExpression(Token boolToken)
        {
            BoolToken = boolToken;    
        }
    }
}
