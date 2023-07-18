using Monkey.Lexing;

namespace Monkey.Parsing;

using PrefixParse = Func<IExpression?>;
using InfixParse = Func<IExpression, IExpression>;

enum Precedence
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
    private readonly Dictionary<Type, PrefixParse> _prefixParseFns = new();
    private readonly Dictionary<Type, InfixParse> _infixParseFns = new();
    
    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _position = 0;
        RegisterPrefix(typeof(IdentToken), ParseIdentifierExpression);
        RegisterPrefix(typeof(IntToken), ParseIntegerExpression);
        RegisterPrefix(typeof(BangToken), ParsePrefixExpression);
        RegisterPrefix(typeof(MinusToken), ParsePrefixExpression);
    }
    
    private Token CurrentToken =>
        _position < _tokens.Count() ? _tokens[_position] : new EofToken();

    private Token PeekToken => 
        _position + 1 < _tokens.Count() ? _tokens[_position + 1] : new EofToken();

    private void ReadToken()
    {
        _position++;
    }

    public Ast ParseProgram()
    {
        Ast program = new();

        while (CurrentToken is not EofToken)
        {
            IStatement? statement = CurrentToken switch
            {
                LetToken => ParseLetStatement(),
                ReturnToken => ParseReturnStatement(),
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

        if (!IsToken<IdentToken>(PeekToken)) return null;
        
        ReadToken();
        IdentifierExpression name = new(CurrentToken, CurrentToken.Literal);

        if (!IsToken<AssignToken>(PeekToken)) return null;
        
        ReadToken();

        while (CurrentToken is not SemicolonToken)
        {
            ReadToken();
        }

        return new(letToken, name, null);
    }

    private ReturnStatement? ParseReturnStatement()
    {
        Token returnToken = CurrentToken;
        
        ReadToken();

        while (CurrentToken is not SemicolonToken)
        {
            ReadToken();
        }

        return new(returnToken, null);
    }

    private ExpressionStatement? ParseExpressionStatement()
    {
        ExpressionStatement expressionStatement = new(
            CurrentToken, 
            ParseExpression(Precedence.Lowest)
        );

        if (PeekToken is SemicolonToken)
        {
            ReadToken();
        }

        return expressionStatement;
    }

    private IExpression? ParseExpression(Precedence precedence)
    {
        PrefixParse? prefix = _prefixParseFns.GetValueOrDefault(CurrentToken.GetType());
        
        if (prefix is null)
        {
            _erros.Add($"No prefix parse function for {CurrentToken.GetType().Name} found");
            return null;
        } 

        IExpression? leftExpression = prefix();

        return leftExpression;
    }

    private IExpression ParseIdentifierExpression()
    {
        return new IdentifierExpression(CurrentToken, CurrentToken.Literal);
    }

    private IExpression? ParseIntegerExpression()
    {
        long value;
        try
        {
            value = long.Parse(CurrentToken.Literal);
        }
        catch (Exception)
        {
            _erros.Add($"Could not parse {CurrentToken.Literal} as integer");
            return null;
        }
        return new IntegerExpression(CurrentToken, value);
    }

    private IExpression ParsePrefixExpression()
    {
        Token prefixToken = CurrentToken;

        ReadToken();
        
        return new PrefixExpression(
            prefixToken,
            prefixToken.Literal,
            ParseExpression(Precedence.Prefix)
        );
    }

    public IEnumerable<string> Errors => _erros.ToList();

    private bool IsToken<T>(Token token)
    {   
        if (token is T)
        {
            return true;
        }
        
        _erros.Add($"Expected next token to be {typeof(T).Name}, got {token} instead");
        return false;
    }

    private void RegisterPrefix(Type type, PrefixParse prefixParse)
    {
        _prefixParseFns.Add(type, prefixParse);
    }
    
    private void RegisterInfix(Type type, InfixParse infixParse)
    {
        _infixParseFns.Add(type, infixParse);
    }
}
