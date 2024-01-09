
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class BinaryExpression : Expression
    {
        public Expression ExpressionLeft;
        public Expression ExpressionRight;
        public Token Operator;
        public override ExpressionType Type => ExpressionType.binaryExpression;
        public BinaryExpression(Expression expressionLeft, Token operatortoken, Expression expressionRight)
        {
            ExpressionLeft = expressionLeft;
            ExpressionRight = expressionRight;
            Operator = operatortoken;

        }
        public object CompNum(double a, double b, TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.LessEqualToken:
                    return a <= b;
                case TokenType.LessToken:
                    return a < b;
                case TokenType.GreaterEqualToken:
                    return a >= b;
                case TokenType.GreaterToken:
                    return a > b;
            }
            return new ErrorExpression("El operador no es correcto");
        }
        public object CalNum(double a, double b, TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.SubtractToken:
                    return a - b;
                case TokenType.PlusToken:
                    return a + b;
                case TokenType.DivideToken:
                    return (b != 0) ? a / b : new ErrorExpression("La division por '0' no esta definida");
                case TokenType.ProductToken:
                    return a * b;
                case TokenType.PowerToken:
                    return Math.Pow(a, b);
            }
            return new ErrorExpression("El operador no es correcto");
        }
        public object OrAnd(bool a, bool b, TokenType tokenType)
        {
            if (tokenType == TokenType.OrToken)
                return a || b;
            else
                return a && b;
        }

    }
}
