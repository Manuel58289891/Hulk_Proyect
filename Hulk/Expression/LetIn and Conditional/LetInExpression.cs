using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class LetInExpression:Expression
    {
        Token LetToken { get; }
        public Dictionary<string, Expression> Variables { get; }
        Token Intoken { get; }
        public Expression Expression { get; }
        public override ExpressionType Type => ExpressionType.LetInExpression;
        public LetInExpression(Token letToken, Dictionary<string, Expression> variables, Token intoken, Expression expression)
        {
            LetToken = letToken;
            Variables = variables;
            Intoken = intoken;
            Expression = expression;
        }
        
    }
}

