using Monkey.Lexing;

namespace Monkey.Parsing;

public class Parser
{
    private readonly List<Token> _tokens;
    private readonly List<string> _erros = new();
    private int _position;
    private Token CurrentToken
    {
        get => _position < _tokens.Count() ? _tokens[_position] : Token.Eof;
    } 
    private Token PeekToken
    {
        get => _position + 1 < _tokens.Count() ? _tokens[_position + 1] : Token.Eof;
    }

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _position = 0;
    }

    private void ReadToken()
    {
        _position++;
    }

    public Ast ParseProgram()
    {
        Ast program = new();

        while (CurrentToken.Type is not TokenType.Eof)
        {
            IStatement? statement = CurrentToken.Type switch
            {
                TokenType.Let => ParseLetStatement(),
                TokenType.Return => ParseReturnStatement(),
                _ => null,
            };
            if (statement != null) program.Statements.Add(statement);
            ReadToken();
        }

        return program;
    }

    private LetStatement? ParseLetStatement()
    {
        Token letToken = CurrentToken;

        Token? identToken = Expected(PeekToken, TokenType.Ident);
        if (identToken is null) return null;
        ReadToken();

        IdentifierExpression name = new(identToken, identToken.Literal);

        if (Expected(PeekToken, TokenType.Assign) is null) return null;
        ReadToken();

        while (CurrentToken.Type is not TokenType.Semicolon)
        {
            ReadToken();
        }

        return new(letToken, name, null);
    }

    private ReturnStatement? ParseReturnStatement()
    {
        Token returnToken = CurrentToken;
        ReadToken();

        while (CurrentToken.Type is not TokenType.Semicolon)
        {
            ReadToken();
        }

        return new(returnToken, null);
    }

    public IEnumerable<string> Errors => _erros.ToList();

    private void ExpectedError(TokenType type, Token peekToken)
    {
        _erros.Add($"Expected next token to be {type}, got {peekToken} instead");
    }

    private Token? Expected(Token token, TokenType type)
    {   
        if (token.Type == type)
        {
            return token;
        }
        else
        {
            ExpectedError(type, token);
            return null;
        }
    }
}
