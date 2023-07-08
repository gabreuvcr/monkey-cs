using Monkey.Lexing;

namespace Monkey.Parsing;

public class Parser
{
    private readonly List<Token> _tokens;
    private readonly List<string> _erros = new();
    private int _position;
    private Token CurrentToken
    {
        get => _position < _tokens.Count() ? _tokens[_position] : new Token.Eof();
    } 
    private Token PeekToken
    {
        get => _position + 1 < _tokens.Count() ? _tokens[_position + 1] : new Token.Eof();
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

        while (CurrentToken is not Token.Eof)
        {
            IStatement? statement = CurrentToken switch
            {
                Token.Let => ParseLetStatement(),
                Token.Return => ParseReturnStatement(),
                _ => null,
            };
            if (statement != null) program.Statements.Add(statement);
            ReadToken();
        }

        return program;
    }

    private LetStatement? ParseLetStatement()
    {
        Token.Let letToken = (Token.Let)CurrentToken;

        Token.Ident? identToken = TryCastTo<Token.Ident>(PeekToken);
        if (identToken is null) return null;
        ReadToken();

        IdentifierExpression name = new(identToken, identToken.Literal);

        if (TryCastTo<Token.Assign>(PeekToken) is null) return null;
        ReadToken();

        while (CurrentToken is not Token.Semicolon)
        {
            ReadToken();
        }

        return new(letToken, name);
    }

    private ReturnStatement? ParseReturnStatement()
    {
        Token.Return returnToken = (Token.Return)CurrentToken;
        ReadToken();

        while (CurrentToken is not Token.Semicolon)
        {
            ReadToken();
        }

        return new(returnToken, null);
    }

    public IEnumerable<string> Errors => _erros.ToList();

    private void CastError<T>(Token peekToken)
    {
        _erros.Add($"Expected next token to be {typeof(T).Name}, got {peekToken} instead");
    }

    private T? TryCastTo<T>(Token token)
    {   
        if (token is T tokenT)
        {
            return tokenT;
        }
        else
        {
            CastError<T>(token);
            return default;
        }
    }
}
