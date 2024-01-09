using ConsoleApp4;


class Program
{
    static void Main()
    {
        while (true)
        {
            var Line = Console.ReadLine();
            if (string.IsNullOrEmpty(Line))
                break;
            var lexer = new Lexer(Line);
            var tokens = lexer.GetTokens();
            var parser = new Parser(tokens);
            var expression = parser.Parse();
            var evaluator = new Evaluator();
            var Ouput = evaluator.EvaluateExpression(expression);

            Console.WriteLine((Ouput is ErrorExpression errorExpression) ? errorExpression.Message : Ouput);
        }



    }
}
