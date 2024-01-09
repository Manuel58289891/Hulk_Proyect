using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class FunctionExpression : Expression
    {
        public string Name { get; }
        public string[] Parametros { get; }
        public Expression Expression { get; }
        public FunctionExpression(string name, string[] parametros, Expression expression)
        {
            Name = name;
            Parametros = parametros;
            Expression = expression;
        }
        public override ExpressionType Type => ExpressionType.FunctionExpression;

    }
}
