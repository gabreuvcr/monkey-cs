using Monkey.Lexing;

namespace Monkey.Parsing;

using PrefixParse = Func<IExpression>;
using InfixParse = Func<IExpression, IExpression>;

enum PrecedenceType
{
    Lowest,
    Equals,
    Comparator,
    Sum,
    Product,
    Prefix,
    Call
}

public class Parser
{
    private readonly List<Token> _tokens;
    private int _position;
    private readonly List<string> _erros = new();
    private readonly Dictionary<TokenType, PrefixParse> _prefixParseFns = new();
    private readonly Dictionary<TokenType, InfixParse> _infixParseFns = new();
    
    private Token CurrentToken =>
        _position < _tokens.Count() ? _tokens[_position] : Token.Eof;

    private Token PeekToken => 
        _position + 1 < _tokens.Count() ? _tokens[_position + 1] : Token.Eof;


    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _position = 0;
        RegisterPrefix(TokenType.Ident, ParseIdentifierExpression);
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
                _ => ParseExpressionStatement(),
            };
            if (statement != null) program.Statements.Add(statement);
            ReadToken();
        }

        return program;
    }

    private LetStatement? ParseLetStatement()
    {
        Token letToken = CurrentToken;

        if (!IsExpected(TokenType.Ident, PeekToken)) return null;
        
        ReadToken();
        IdentifierExpression name = new(CurrentToken, CurrentToken.Literal);

        if (!IsExpected(TokenType.Assign, PeekToken)) return null;
        
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

    private ExpressionStatement? ParseExpressionStatement()
    {
        ExpressionStatement expressionStatement = new(
            CurrentToken, 
            ParseExpression(PrecedenceType.Lowest)
        );

        if (PeekToken.Type is TokenType.Semicolon)
        {
            ReadToken();
        }

        return expressionStatement;
    }

    private IExpression? ParseExpression(PrecedenceType precedenceType)
    {
        PrefixParse? prefix = _prefixParseFns.GetValueOrDefault(CurrentToken.Type);
        
        if (prefix is null) return null;

        IExpression leftExpression = prefix();

        return leftExpression;
    }

    private IExpression ParseIdentifierExpression()
    {
        return new IdentifierExpression(CurrentToken, CurrentToken.Literal);
    }

    public IEnumerable<string> Errors => _erros.ToList();

    private void AddError(TokenType expectedType, Token peekToken)
    {
        _erros.Add($"Expected next token to be {expectedType}, got {peekToken} instead");
    }

    private bool IsExpected(TokenType expectedType, Token token)
    {   
        if (token.Type == expectedType)
        {
            return true;
        }
        
        AddError(expectedType, token);
        return false;
    }

    private void RegisterPrefix(TokenType type, PrefixParse prefixParse)
    {
        _prefixParseFns.Add(type, prefixParse);
    }
    
    private void RegisterInfix(TokenType type, InfixParse infixParse)
    {
        _infixParseFns.Add(type, infixParse);
    }
}
