using Monkey.Lexing;

namespace Monkey.Parsing;

public interface INode
{
    string TokenLiteral();    
}

public interface IStatement : INode {}
public interface IExpression : INode {}

public class Ast : INode
{
    public readonly List<IStatement> Statements = new();

    public string TokenLiteral()
    {
        if (Statements.Count > 0) return Statements[0].TokenLiteral();

        return string.Empty;
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
}

public class LetStatement : IStatement
{
    public Token Token;
    public IdentifierExpression Name;
    public IExpression? Value;

    public LetStatement(Token token, IdentifierExpression name)
    {
        Token = token;
        Name = name;
    }

    public string TokenLiteral() => Token.Literal;
}
