using System.Text;
using Monkey.Lexing;

namespace Monkey.Parsing;

public interface IExpression : INode {}

public class IdentifierExpression : IExpression
{
    public Token Token;
    public string Value;

    public IdentifierExpression(Token token, string value)
    {
        Token = token;
        Value = value;
    }

    public string TokenLiteral() => Token.Literal;

    public override string ToString() => Value;
}

public class IntegerExpression : IExpression
{
    public Token Token;
    public long Value;

    public IntegerExpression(Token token, long value)
    {
        Token = token;
        Value = value;
    }

    public string TokenLiteral() => Token.Literal;

    public override string ToString() => Token.Literal;
}

public class PrefixExpression : IExpression
{
    public Token Token;
    public string Operator;
    public IExpression? Right;

    public PrefixExpression(Token token, string op, IExpression? right)
    {
        Token = token;
        Operator = op;
        Right = right;
    }

    public string TokenLiteral() => Token.Literal;

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.Append("(");
        sb.Append(Operator);
        sb.Append(Right?.ToString());
        sb.Append(")");

        return sb.ToString();
    }
}
