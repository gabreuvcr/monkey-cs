using Monkey.Lexing;

namespace Monkey.Parsing;

public class Parser
{
    private readonly Lexer _lexer;
    private Token _currentToken = new Token.Eof();
    private Token _peekToken = new Token.Eof();
    private readonly List<string> _erros = new();

    public Parser(Lexer lexer)
    {
        _lexer = lexer;
        NextToken();
        NextToken();
    }

    private void NextToken()
    {
        _currentToken = _peekToken;
        _peekToken = _lexer.NextToken();
    }

    public Ast ParseProgram()
    {
        Ast program = new();

        while (_currentToken is not Token.Eof)
        {
            IStatement? statement = _currentToken switch
            {
                Token.Let => ParseLetStatement(),
                _ => null,
            };
            if (statement != null) program.Statements.Add(statement);
            NextToken();
        }

        return program;
    }

    private LetStatement? ParseLetStatement()
    {
        Token.Let letToken = (Token.Let)_currentToken;

        Token.Ident? identToken = TryCastTo<Token.Ident>(_peekToken);
        if (identToken is null) return null;

        IdentifierExpression name = new(identToken, identToken.Literal);

        if (TryCastTo<Token.Assign>(_peekToken) is null) return null;

        while (_currentToken is not Token.Semicolon)
        {
            NextToken();
        }

        return new(letToken, name);
    }

    public IEnumerable<string> Errors => _erros.ToList();

    private void CastError<T>(Token peekToken)
    {
        _erros.Add($"Expected next token to be {typeof(T)}, got {peekToken.GetType()} instead");
    }

    private T? TryCastTo<T>(Token token)
    {   
        if (token is T tokenT)
        {
            NextToken();
            return tokenT;
        }
        else
        {
            CastError<T>(token);
            return default;
        }
    }
}
