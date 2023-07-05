using Monkey.Lexing;

namespace Monkey.Parsing;

public class Parser
{
    private readonly Lexer _lexer;
    private Token _currentToken = Token.Empty;
    private Token _peekToken = Token.Empty;
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

        while (_currentToken.Type != TokenType.Eof)
        {
            IStatement? statement = _currentToken.Type switch
            {
                TokenType.Let => ParseLetStatement(),
                _ => null,
            };
            if (statement != null) program.Statements.Add(statement);
            NextToken();
        }

        return program;
    }

    private LetStatement? ParseLetStatement()
    {
        Token token = _currentToken;

        if (!ExpectPeek(TokenType.Ident)) return null;

        IdentifierExpression name = new(_currentToken, _currentToken.Literal);

        if (!ExpectPeek(TokenType.Assign)) return null;

        while (!CurrentTokenIs(TokenType.Semicolon))
        {
            NextToken();
        }

        return new(token, name);
    }

    public IEnumerable<string> Errors => _erros.ToList();

    private void PeekError(TokenType tokenType)
    {
        _erros.Add($"Expected next token to be {tokenType}, got {_peekToken} instead");
    }

    private bool CurrentTokenIs(TokenType tokenType)
    {
        return _currentToken.Type == tokenType;
    }

    private bool PeekTokenIs(TokenType tokenType)
    {
        return _peekToken.Type == tokenType;
    }

    private bool ExpectPeek(TokenType tokenType)
    {
        if (PeekTokenIs(tokenType))
        {
            NextToken();
            return true;
        }
        else
        {
            PeekError(tokenType);
            return false;
        }
    }
}