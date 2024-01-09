namespace ConsoleApp4
{


    static class Global
    {
        public static Dictionary<string, FunctionExpression> Functions = new Dictionary<string, FunctionExpression>();
        public static List<Dictionary<string, Expression>> scope = new List<Dictionary<string, Expression>>();
        public static Stack<Dictionary<string, object>> call = new Stack<Dictionary<string, object>>();
        public static Dictionary<string, Expression> Variables = new Dictionary<string, Expression>();
    }
}