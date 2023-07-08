using Monkey.Lexing;

namespace Monkey.Parsing;

public class Parser
{
    private readonly List<Token> _tokens;
    private readonly List<string> _erros = new();
    private int _currPosition;
    private int _peekPosition;
    private Token CurrToken
    {
        get => _currPosition < _tokens.Count() ? _tokens[_currPosition] : new Token.Eof();
    } 
    private Token PeekToken
    {
        get => _peekPosition < _tokens.Count() ? _tokens[_peekPosition] : new Token.Eof();
    }

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _currPosition = 0;
        _peekPosition = 1;
    }

    private void ReadToken()
    {
        _currPosition = _peekPosition;
        _peekPosition++;
    }

    public Ast ParseProgram()
    {
        Ast program = new();

        while (CurrToken is not Token.Eof)
        {
            IStatement? statement = CurrToken switch
            {
                Token.Let => ParseLetStatement(),
                _ => null,
            };
            if (statement != null) program.Statements.Add(statement);
            ReadToken();
        }

        return program;
    }

    private LetStatement? ParseLetStatement()
    {
        Token.Let letToken = (Token.Let)CurrToken;

        Token.Ident? identToken = TryCastTo<Token.Ident>(PeekToken);
        if (identToken is null) return null;

        IdentifierExpression name = new(identToken, identToken.Literal);

        if (TryCastTo<Token.Assign>(PeekToken) is null) return null;

        while (CurrToken is not Token.Semicolon)
        {
            ReadToken();
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
            ReadToken();
            return tokenT;
        }
        else
        {
            CastError<T>(token);
            return default;
        }
    }
}
