using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4

{
    public enum TokenType
    {
        NumberToken,
        PlusToken,
        SubtractToken,
        InvalidToken,
        EndToken,
        SemiColonToken,
        ProductToken,
        DivideToken,
        PowerToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        WordToken,
        TrueToken,
        FalseToken,
        LetToken,
        InToken,
        IfToken,
        ElseToken,
        SenToken,
        CosToken,
        TanToken,
        LogToken,
        PrintToken,
        FunctionNameToken,
        VariableToken,
        AndToken,
        OrToken,
        EqualEqualToken,
        EqualToken,
        GreaterEqualToken,
        LessEqualToken,
        GreaterToken,
        LessToken,
        CommaToken,
        FunctionToken,
        LambdaToken,
        ConcatToken,
        StringToken,
        NotToken,
        NotEqualToken,
    }
}
