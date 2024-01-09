using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class CallFunctionExpression:Expression
    {
        public string Name;
        public Expression[] Parametros;
        public override ExpressionType Type => ExpressionType.CallFunctionExpression;
        public CallFunctionExpression(string name, Expression[] parametros)
        {
            Name= name;
            Parametros = parametros;
                
        }
    }
}
