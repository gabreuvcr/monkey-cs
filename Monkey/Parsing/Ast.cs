using System.Text;
using Monkey.Lexing;

namespace Monkey.Parsing;

public interface INode
{
    string TokenLiteral();    
    string ToString();
}

public interface IStatement : INode {}
public interface IExpression : INode {}

public class Ast : INode
{
    public readonly List<IStatement> Statements = new();

    public Ast() { }

    public Ast(List<IStatement> statements)
    {
        Statements = statements;
    }

    public string TokenLiteral()
    {
        if (Statements.Count > 0) return Statements[0].TokenLiteral();

        return string.Empty;
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        foreach (IStatement statement in Statements)
        {
            sb.Append(statement.ToString());
        }

        return sb.ToString();
    }
}

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

public class LetStatement : IStatement
{
    public Token Token;
    public IdentifierExpression Name;
    public IExpression? Value;

    public LetStatement(Token token, IdentifierExpression name, IExpression? value)
    {
        Token = token;
        Name = name;
        Value = value;
    }

    public string TokenLiteral() => Token.Literal;

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"{TokenLiteral()} ");
        sb.Append($"{Name.ToString()}");
        sb.Append(" = ");

        if (Value is not null)
        {
            sb.Append(Value.ToString());
        }
        sb.Append(";");

        return sb.ToString();
    }
}

public class ReturnStatement : IStatement
{
    public Token Token;
    public IExpression? Value;

    public ReturnStatement(Token token, IExpression? value)
    {
        Token = token;
        Value = value;
    }

    public string TokenLiteral() => Token.Literal;

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"{TokenLiteral()} ");
        
        if (Value is not null)
        {
            sb.Append(Value.ToString());
        }
        sb.Append(";");

        return sb.ToString();
    }
}

public class ExpressionStatement : IStatement
{
    public Token Token;
    public IExpression? Expression;

    public ExpressionStatement(Token token, IExpression? expression)
    {
        Token = token;
        Expression = expression;
    }

    public string TokenLiteral() => Token.Literal;

    public override string ToString()
    {
        if (Expression is not null)
        {
            return Expression.ToString();
        }
        return "";
    }
}
