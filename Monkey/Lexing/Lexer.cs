namespace Monkey.Lexing;

public class Lexer
{
    private readonly string _input;
    private int _position;

    public Lexer(string input)
    {
        _input = input;
        _position = 0;
    }

    private char CurrentChar =>
        _position < _input.Length ? _input[_position] : '\0';

    private char PeekChar =>
        _position + 1 < _input.Length ? _input[_position + 1] : '\0';

    public List<Token> TokenizeProgram()
    {
        List<Token> tokens = new();

        while (CurrentChar is not '\0')
        {
            tokens.Add(NextToken());
        }
        tokens.Add(NextToken());

        return tokens;
    }

    public Token NextToken()
    {
        SkipWhitespace();

        if (char.IsAsciiDigit(CurrentChar))
        {
            return new IntToken(ReadInteger());
        }

        if (char.IsAsciiLetter(CurrentChar) || CurrentChar == '_')
        {
            return ReadIdentifier() switch
            {
                "fn" => new FunctionToken(),
                "let" => new LetToken(),
                "if" => new IfToken(),
                "else" => new ElseToken(),
                "return" => new ReturnToken(),
                "true" => new TrueToken(),
                "false" => new FalseToken(),
                string ident => new IdentToken(ident),
            };
        }

        var token = CurrentChar switch
        {
            '=' => PeekChar switch
            {
                '=' => SkipPeek(new EqualToken()),
                _ => new AssignToken(),
            },
            '!' => PeekChar switch
            {
                '=' => SkipPeek(new NotEqualToken()),
                _ => new BangToken(),
            },
            '-' => new MinusToken(),
            '+' => new PlusToken(),
            '/' => new SlashToken(),
            '*' => new AsteriskToken(),
            '<' => new LessThanToken(),
            '>' => new GreaterThanToken(),
            ';' => new SemicolonToken(),
            ',' => new CommaToken(),
            '(' => new LeftParenToken(),
            ')' => new RightParenToken(),
            '{' => new LeftBraceToken(),
            '}' => new RightBraceToken(),
            '\0' => new EofToken(),
            _ => new IllegalToken(CurrentChar),
        };

        ReadChar();
        return token;
    }

    private void ReadChar()
    {
        _position++;
    }

    private string ReadIdentifier()
    {
        int position = _position;

        while (char.IsAsciiLetterOrDigit(CurrentChar) || CurrentChar == '_')
        {
            ReadChar();
        }

        return _input[position.._position];
    }

    private string ReadInteger()
    {
        int position = _position;

        while (char.IsAsciiDigit(CurrentChar))
        {
            ReadChar();
        }

        return _input[position.._position];
    }

    private void SkipWhitespace()
    {
        while (char.IsWhiteSpace(CurrentChar))
        {
            ReadChar();
        }
    }

    private Token SkipPeek(Token token)
    {
        ReadChar();
        return token;
    }
}
