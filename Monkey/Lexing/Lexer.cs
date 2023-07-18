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
            return Tokens.Int(ReadInteger());
        }

        if (char.IsAsciiLetter(CurrentChar) || CurrentChar == '_')
        {
            return ReadIdentifier() switch
            {
                "fn" => Tokens.Function,
                "let" => Tokens.Let,
                "if" => Tokens.If,
                "else" => Tokens.Else,
                "return" => Tokens.Return,
                "true" => Tokens.True,
                "false" => Tokens.False,
                string ident => Tokens.Ident(ident),
            };
        }

        var token = CurrentChar switch
        {
            '=' => PeekChar switch
            {
                '=' => SkipPeek(Tokens.Equal),
                _ => Tokens.Assign,
            },
            '!' => PeekChar switch
            {
                '=' => SkipPeek(Tokens.NotEqual),
                _ => Tokens.Bang,
            },
            '-' => Tokens.Minus,
            '+' => Tokens.Plus,
            '/' => Tokens.Slash,
            '*' => Tokens.Asterisk,
            '<' => Tokens.LessThan,
            '>' => Tokens.GreaterThan,
            ';' => Tokens.Semicolon,
            ',' => Tokens.Comma,
            '(' => Tokens.LeftParen,
            ')' => Tokens.RightParen,
            '{' => Tokens.LeftBrace,
            '}' => Tokens.RightBrace,
            '\0' => Tokens.Eof,
            _ => Tokens.Illegal(CurrentChar),
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
