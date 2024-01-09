using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Lexer
    {
        private readonly string _text;
        private int pos;
        private char Current
        {
            get
            {
                if (pos >= _text.Length)
                {
                    return '\0';
                }
                return _text[pos];
            }


        }
        public Lexer(string text)
        {
            _text = text;
        }

        public Token GetToken()

        {
            while (char.IsWhiteSpace(Current))
            {
                pos++;
            }
            if (char.IsLetter(Current))
            {
                string word = "";
                while (char.IsLetter(Current))
                {
                    word += Current;
                    pos++;
                }
                pos--;

                switch (word)
                {
                    case "function":
                        return new Token(word, TokenType.FunctionToken);
                    case "true":
                    case "True":
                        return new Token(word, TokenType.TrueToken);
                    case "false":
                    case "False":
                        return new Token(word, TokenType.FalseToken);
                    case "let":
                        return new Token(word, TokenType.LetToken);
                    case "in":
                        return new Token(word, TokenType.InToken);
                    case "if":
                        return new Token(word, TokenType.IfToken);
                    case "else":
                        return new Token(word, TokenType.ElseToken);
                    case "sin":
                        return new Token(word, TokenType.SenToken);
                    case "cos":
                        return new Token(word, TokenType.CosToken);
                    case "tan":
                        return new Token(word, TokenType.TanToken);
                    case "log":
                        return new Token(word, TokenType.LogToken);
                    case "print":
                        return new Token(word, TokenType.PrintToken);
                    default:
                        pos++;
                        while (char.IsWhiteSpace(Current))
                        {
                            pos++;
                        }
                        if (Current == '(')
                        {
                            pos--;
                            return new Token(word, TokenType.FunctionNameToken);

                        }
                        else
                        {
                            pos--;
                            return new Token(word, TokenType.VariableToken);
                        }
                }

            }
            if (char.IsDigit(Current))
            {
                string number = "";

                while (char.IsDigit(Current))
                {
                    number += Current;
                    pos++;
                }
                pos--;
                return new Token(number, TokenType.NumberToken);

            }

            switch (Current)
            {
                case '>':
                    pos++;
                    if (Current == '=')
                    {
                        return new Token(">=", TokenType.GreaterEqualToken);
                    }
                    pos--;
                    return new Token(">", TokenType.GreaterToken);
                case '<':
                    pos++;
                    if (Current == '=')
                    {
                        return new Token("<=", TokenType.LessEqualToken);
                    }
                    pos--;
                    return new Token("<", TokenType.LessToken);

                case '=':
                    pos++;
                    if (Current == '=')
                    {
                        return new Token("==", TokenType.EqualEqualToken);
                    }
                    else if (Current == '>')
                    {
                        return new Token("=>", TokenType.LambdaToken);
                    }

                    pos--;
                    return new Token("=", TokenType.EqualToken);
                case '!':
                    pos++;
                    if (Current == '=')
                    {
                        return new Token("!=", TokenType.NotEqualToken);
                    }
                    else pos--;
                    return new Token("!", TokenType.NotToken);

                case '+':

                    return new Token("+", TokenType.PlusToken);
                case '-':

                    return new Token("-", TokenType.SubtractToken);
                case '/':

                    return new Token("/", TokenType.DivideToken);
                case '*':

                    return new Token("*", TokenType.ProductToken);
                case '^':

                    return new Token("^", TokenType.PowerToken);
                case '@':
                    return new Token("@", TokenType.ConcatToken);
                case '(':

                    return new Token("(", TokenType.OpenParenthesisToken);
                case ')':

                    return new Token(")", TokenType.CloseParenthesisToken);
                case '&':
                    return new Token("&", TokenType.AndToken);
                case '|':
                    return new Token("|", TokenType.OrToken);
                case ',':
                    return new Token(",", TokenType.CommaToken);
                case ';':
                    return new Token(";", TokenType.SemiColonToken);
                case '"':
                    pos++;
                    string str = "";
                    while (Current!='"')
                    {
                        str += Current;
                        pos++;
                    }
                    return new Token(str, TokenType.StringToken);

                default:
                    return new Token("" + Current, TokenType.InvalidToken);

            }


        }
        public Token[] GetTokens()
        {
            var tokens = new List<Token>();
            while (pos <= _text.Length)
            {
                tokens.Add(GetToken());
                pos++;
            }
            return tokens.ToArray();

        }
    }
}
