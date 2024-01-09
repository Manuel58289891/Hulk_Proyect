namespace ConsoleApp4
{
    class UnaryExpression : Expression
    {
        public Token Left { get; }
        public Expression Right { get; }
        public override ExpressionType Type => throw new NotImplementedException();

        public UnaryExpression(Token left, Expression right)
        {
            Left = left;
            Right = right;
        }
    }
}