using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Evaluator
    {

        public object EvaluateExpression(Expression expression)
        {
            if (expression is ErrorExpression errorExpression)
            {
                return errorExpression.Message;
            }
            if (expression is SenExpression senExpression)
            {
                try
                {
                    return Math.Sin((double)EvaluateExpression(senExpression.value));
                }
                catch (InvalidCastException)
                {
                    return new ErrorExpression("La funcion Log solo esta definida para valores double");
                }
            }
            if (expression is CosExpression cosExpression)
            {
                try
                {
                    return Math.Cos((double)EvaluateExpression(cosExpression.value));
                }
                catch (InvalidCastException)
                {
                    return new ErrorExpression("La funcion Log solo esta definida para valores double");
                }
            }
            if (expression is TanExpression tanExpression)
            {
                try
                {
                    return Math.Tan((double)EvaluateExpression(tanExpression.value));
                }
                catch (InvalidCastException)
                {
                    return new ErrorExpression("La funcion Log solo esta definida para valores double");
                }
            }
            if (expression is PrintExpression printExpression)
            {
                return EvaluateExpression(printExpression.value);
            }
            if (expression is LogExpression logExpression)
            {
                try
                {
                    return Math.Log((double)EvaluateExpression(logExpression.value1), (double)EvaluateExpression(logExpression.value2));
                }
                catch (InvalidCastException)
                {
                    return new ErrorExpression("La funcion Log solo esta definida para valores double");
                }
            }
            if (expression is VariableExpression variable)
            {
                int i = 1;
                while (Global.scope.Count >= i)
                {
                    if (Global.scope[Global.scope.Count - i].ContainsKey(variable.Name))
                        return EvaluateExpression(Global.scope[Global.scope.Count - i][variable.Name]);
                    else i++;
                }
                return new ErrorExpression($"La variable '{variable.Name}' no existe en el contexto actual");

            }
            if (expression is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Left.Type == TokenType.SubtractToken)
                    try
                    {
                        return -(double)EvaluateExpression(unaryExpression.Right);
                    }
                    catch (InvalidCastException)
                    {
                        return new ErrorExpression("El operador '-' solo se puede usar en numeros");
                    }

                else if (unaryExpression.Left.Type == TokenType.NotToken)
                    try
                    {
                        return !(bool)EvaluateExpression(unaryExpression.Right);
                    }
                    catch (InvalidCastException)
                    {
                        return new ErrorExpression("El operador '!' solo se puede usar en booleanos");
                    }

                else return EvaluateExpression(unaryExpression.Right);
            }
            if (expression is StringExpression stringExpression)
            {
                return stringExpression.StringToken.Text;
            }
            if (expression is NumberExpression Number)
            {
                return double.Parse(Number.NumberToken.Text);
            }
            if (expression is ParenthesisExpression Expression)
            {
                return EvaluateExpression(Expression.Expression);
            }
            if (expression is BoolExpression boolExpression)
            {
                return boolExpression.BoolToken.Text == "true";
            }
            if (expression is ConditionalExpression Conditional)
            {
                if ((bool)EvaluateExpression(Conditional.IfExpression))
                {
                    return EvaluateExpression(Conditional.Expression);
                }
                else return EvaluateExpression(Conditional.ElseExpression);
            }
            if (expression is LetInExpression letInExpression)
            {
                Global.scope.Add(letInExpression.Variables);
                var InExpression = EvaluateExpression(letInExpression.Expression);
                if (Global.scope.Count > 0)
                {
                    Global.scope.RemoveAt(Global.scope.Count - 1);
                }
                return InExpression;

            }
            if (expression is FunctionVariableExpression functionVariable)
            {
                return Global.call.Peek()[functionVariable.Name];
            }
            if (expression is FunctionExpression functionExpression)
            {
                Global.Functions.Add(functionExpression.Name, functionExpression);
                return "";
            }
            if (expression is CallFunctionExpression callfunctionExpression)
            {
                Dictionary<string, object> CurrentParametros = new Dictionary<string, object>();
                try
                {
                    var function = Global.Functions[callfunctionExpression.Name];
                    for (int i = 0; i < function.Parametros.Length; i++)
                    {
                        CurrentParametros.Add(function.Parametros[i], EvaluateExpression(callfunctionExpression.Parametros[i]));
                    }
                    Global.call.Push(CurrentParametros);
                    var expressionToReturn = EvaluateExpression(function.Expression);
                    Global.call.Pop();
                    return expressionToReturn;
                }
                catch (KeyNotFoundException)
                {
                    return new ErrorExpression($"La funcion '{callfunctionExpression.Name}'no ha sido declarada");
                }
                
            }
            else if (expression is BinaryExpression binaryExpression)
            {
                var left = EvaluateExpression(binaryExpression.ExpressionLeft);
                var right = EvaluateExpression(binaryExpression.ExpressionRight);
                if (left is ErrorExpression)
                    return left;
                if (right is ErrorExpression)
                    return right;
                switch (binaryExpression.Operator.Type)
                {

                    case TokenType.LessEqualToken:
                    case TokenType.LessToken:
                    case TokenType.GreaterEqualToken:
                    case TokenType.GreaterToken:
                        try
                        {
                            return binaryExpression.CompNum((double)left, (double)right, binaryExpression.Operator.Type);
                        }
                        catch (InvalidCastException)
                        {
                            return new ErrorExpression($"El operador '{binaryExpression.Operator.Text}' solo esta definido para numeros");
                        }

                    case TokenType.EqualEqualToken:
                        return left.Equals(right);
                    case TokenType.NotEqualToken:
                        return !left.Equals(right);

                    case TokenType.PlusToken:
                    case TokenType.SubtractToken:
                    case TokenType.DivideToken:
                    case TokenType.ProductToken:
                    case TokenType.PowerToken:
                        try
                        {
                            return binaryExpression.CalNum((double)left, (double)right, binaryExpression.Operator.Type);
                        }
                        catch (InvalidCastException)
                        {
                            return new ErrorExpression($"El operador '{binaryExpression.Operator.Text}' solo esta definido para numeros");
                        }

                    case TokenType.OrToken:
                    case TokenType.AndToken:
                        try
                        {
                            return binaryExpression.OrAnd((bool)left, (bool)right, binaryExpression.Operator.Type);
                        }
                        catch (InvalidCastException)
                        {
                            return new ErrorExpression($"El operador '{binaryExpression.Operator.Text}' solo esta definido para bool");
                        }
                    case TokenType.ConcatToken:
                        try
                        {
                            return (string)left + (string)right;
                        }
                        catch (InvalidCastException)
                        {
                            return new ErrorExpression($"El operador '{binaryExpression.Operator.Text}' solo esta definido para strings");
                        }


                }
            }
            return new ErrorExpression("Expression desconocida");
        }
    }
}
