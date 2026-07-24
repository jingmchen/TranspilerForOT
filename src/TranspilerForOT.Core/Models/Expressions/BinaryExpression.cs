using TranspilerForOT.Core.Data;

namespace TranspilerForOT.Core.Models.Expressions;

public sealed class BinaryExpression : Expression
{
    public BinaryOperator Operator {get;}
    public Expression Left {get;}
    public Expression Right {get;}

    public BinaryExpression(BinaryOperator op, Expression left, Expression right)
    {
        Operator = op;
        Left = left;
        Right = right;
    }
}