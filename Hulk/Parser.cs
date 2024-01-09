
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ConsoleApp4
{

    internal class Parser
    {
        private readonly Token[] _tokens = new Token[33];
        private int pos;
        bool IsFunctionVariable = false;
        private Token Current
        {
            get
            {
                if (pos >= _tokens.Length)
                {
                    return new Token("\0", TokenType.EndToken);
                }
                return _tokens[pos];
            }
        }
        public Parser(Token[] tokens)
        {
            _tokens = tokens;

        }
        public Expression A()
        {
            var left = N();
            if (Current.Type != TokenType.EndToken && Current.Type != TokenType.SemiColonToken)
            {
                while (Current.Type == TokenType.AndToken)
                {
                    var operatortoken = Current;
                    pos++;
                    var right = N();
                    left = new BinaryExpression(left, operatortoken, right);
                }

            }
            return left;
        }
        public Expression Parse()
        {
            if (_tokens[_tokens.Length - 2].Type != TokenType.SemiColonToken)
            {
                return new ErrorExpression("Falta ';'");
            }
            var left = A();
            if (Current.Type != TokenType.EndToken && Current.Type != TokenType.SemiColonToken)
            {
                while (Current.Type == TokenType.OrToken)
                {
                    var operatortoken = Current;
                    pos++;
                    var right = A();
                    left = new BinaryExpression(left, operatortoken, right);
                }

            }
            return left;
        }
        public Expression E(Token currentToken)
        {
            pos++;
            if (currentToken.Type == TokenType.SubtractToken || currentToken.Type == TokenType.PlusToken)
            {
                var left = currentToken;

                var expression = E(Current);

                return new UnaryExpression(left, expression);
            }
            if (currentToken.Type == TokenType.StringToken)
            {
                return new StringExpression(currentToken);
            }
            if (currentToken.Type == TokenType.TrueToken || currentToken.Type == TokenType.FalseToken)
            {
                return new BoolExpression(currentToken);
            }
            if (currentToken.Type == TokenType.NumberToken)
            {
                return new NumberExpression(currentToken);
            }
            if (currentToken.Type == TokenType.OpenParenthesisToken)
            {
                var parenthesisExpresssion = Parse();
                pos++;
                return new ParenthesisExpression(parenthesisExpresssion);
            }
            else if (currentToken.Type == TokenType.VariableToken)
            {

                if (IsFunctionVariable)
                {
                    return new FunctionVariableExpression(currentToken.Text);
                }
                return new VariableExpression(currentToken.Text);

            }


            if (currentToken.Type == TokenType.IfToken)
            {
                pos++;
                var ifExpression = Parse();
                pos++;
                var expression = Parse();
                pos++;
                var elseExpression = Parse();

                return new ConditionalExpression(ifExpression, expression, elseExpression);

            }
            else if (currentToken.Type == TokenType.LetToken)
            {
                var LetVariables = new Dictionary<string, Expression>();

                var let = currentToken;
                pos--;

                do
                {
                    pos++;
                    if (Current.Type == TokenType.VariableToken)
                    {
                        if (Current.Type == TokenType.VariableToken)
                        {
                            var variablename = Current.Text;
                            pos++;
                            if (Current.Type == TokenType.EqualToken)
                            {
                                pos++;
                                if (Current.Type == TokenType.InToken)
                                {
                                    return new ErrorExpression("Missing Value");
                                }
                                else
                                {

                                    if (LetVariables.ContainsKey(variablename))
                                    {
                                        LetVariables[variablename] = Parse();
                                    }
                                    else
                                    {
                                        LetVariables.Add(variablename, Parse());
                                    }

                                }
                            }

                        }
                    }
                    else return new ErrorExpression($"'{Current.Text}' no es un nombre valido para una variable");
                }
                while (Current.Type == TokenType.CommaToken);

                if (Current.Type == TokenType.InToken)
                {
                    var intoken = Current; pos++;
                    var expression = Parse();
                    return new LetInExpression(let, LetVariables, intoken, expression);
                }
                else return new ErrorExpression("Missin 'in'");
            }
            if (currentToken.Type == TokenType.FunctionToken)
            {

                var functionName = Current.Text;
                pos++;
                var parametros = new List<string>();
                if (Current.Type == TokenType.OpenParenthesisToken)
                {

                    do
                    {
                        pos++;
                        parametros.Add(Current.Text);
                        pos++;
                    } while (Current.Type == TokenType.CommaToken);
                    if (Current.Type == TokenType.CloseParenthesisToken)
                    {
                        pos++;
                        if (Current.Type == TokenType.LambdaToken)
                        {
                            pos++;
                            IsFunctionVariable = true;
                            var expression = Parse();
                            IsFunctionVariable = false;
                            return new FunctionExpression(functionName, parametros.ToArray(), expression);
                        }
                        //let x = 5 in x
                        //function d(n) => n;
                    }

                }
           
                if (currentToken.Type == TokenType.LogToken)
                {
                    var arguments = new Expression[2];
                    if (Current.Type != TokenType.OpenParenthesisToken)
                    {
                        pos++;
                        arguments[0] = Parse();
                        pos++;
                        arguments[1] = Parse();

                        if (Current.Type == TokenType.CloseParenthesisToken)
                        {
                            pos++;
                            return new LogExpression(arguments[0], arguments[1]);
                        }
                        else return new ErrorExpression("Se esperaba ')'");
                    }
                    else return new ErrorExpression("Se esperaba '('");
                }
            }
            if (currentToken.Type == TokenType.SenToken || currentToken.Type == TokenType.CosToken ||
               currentToken.Type == TokenType.TanToken || currentToken.Type == TokenType.PrintToken)
            {
                if (Current.Type == TokenType.OpenParenthesisToken)
                {

                    pos++;
                    var argument = Parse();

                    if (Current.Type == TokenType.CloseParenthesisToken)
                    {

                        pos++;
                        switch (currentToken.Type)
                        {
                            case TokenType.SenToken:
                                return new SenExpression(argument);

                            case TokenType.CosToken:
                                return new CosExpression(argument);

                            case TokenType.TanToken:
                                return new TanExpression(argument);

                            case TokenType.PrintToken:
                                return new PrintExpression(argument);

                        }
                    }
                    else return new ErrorExpression("Se esperaba ')'");
                }
                else return new ErrorExpression("Se esperaba '('");
            }
            if (currentToken.Type == TokenType.FunctionNameToken)
            {
                var functionName = currentToken.Text;

                if (Current.Type == TokenType.OpenParenthesisToken)
                {
                    var arr = new List<Expression>();

                    do
                    {
                        pos++;
                        arr.Add(Parse());

                    } while (Current.Type == TokenType.CommaToken);
                    if (Current.Type == TokenType.CloseParenthesisToken)
                    {
                        pos++;
                        return new CallFunctionExpression(functionName, arr.ToArray());
                    }
                }

            }

            return new ErrorExpression($"unexpected token {Current.Text}");

        }
        public Expression N()
        {
            var left = T();
            if (Current.Type != TokenType.EndToken && Current.Type != TokenType.SemiColonToken)
            {
                while (Current.Type == TokenType.EqualEqualToken || Current.Type == TokenType.GreaterEqualToken || Current.Type == TokenType.LessEqualToken || Current.Type == TokenType.LessToken || Current.Type == TokenType.GreaterToken || Current.Type == TokenType.NotEqualToken)
                {
                    var operatortoken = Current;
                    pos++;
                    var right = T();
                    left = new BinaryExpression(left, operatortoken, right);

                }

            }
            return left;
        }
        public Expression F()
        {
            var left = P();
            if (Current.Type != TokenType.EndToken && Current.Type != TokenType.SemiColonToken)
            {
                while (Current.Type == TokenType.ProductToken || Current.Type == TokenType.DivideToken)
                {
                    var operatortoken = Current;
                    pos++;
                    var right = P();
                    left = new BinaryExpression(left, operatortoken, right);
                }

            }
            return left;
        }
        public Expression T()
        {
            var left = F();
            if (Current.Type != TokenType.EndToken && Current.Type != TokenType.SemiColonToken)
            {
                while (Current.Type == TokenType.PlusToken || Current.Type == TokenType.SubtractToken || Current.Type == TokenType.ConcatToken)
                {
                    var operatortoken = Current;
                    pos++;
                    var right = F();
                    left = new BinaryExpression(left, operatortoken, right);

                }
            }
            return left;
        }
        public Expression P()
        {
            var left = E(Current);
            if (Current.Type != TokenType.EndToken && Current.Type != TokenType.SemiColonToken)
            {
                while (Current.Type == TokenType.PowerToken)
                {
                    var operatortoken = Current;
                    pos++;
                    var right = E(Current);
                    left = new BinaryExpression(left, operatortoken, right);
                }

            }
            return left;


        }

    }



}


