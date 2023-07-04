using System.Security.Principal;

namespace Monkey.Lexing;

public class Lexer
{
    private readonly string _input;
    private int _position;
    private int _readPostion;
    private char _ch;

    public Lexer(string input)
    {
        _input = input;
        ReadChar();
    }

    public Token NextToken()
    {
        SkipWhitespace();

        if (char.IsAsciiDigit(_ch)) 
        {
            return new Token.Int(ReadInteger());
        }
        
        if (char.IsAsciiLetter(_ch) || _ch == '_')
        {
            return ReadIdentifier() switch
            {
                "fn" => new Token.Function(),
                "let" => new Token.Let(),
                "if" => new Token.If(),
                "else" => new Token.Else(),
                "return" => new Token.Return(),
                "true" => new Token.True(),
                "false" => new Token.False(),
                string ident => new Token.Ident(ident),
            };
        }

        Token token = _ch switch
        {
            '=' => PeekChar() switch
            {
                '=' => new Token.Equal(),
                _ => new Token.Assign(),
            },
            '!' => PeekChar() switch
            {
                '=' => new Token.NotEqual(),
                _ => new Token.Bang(),
            },
            '-' => new Token.Minus(),
            '+' => new Token.Plus(),
            '/' => new Token.Slash(),
            '*' => new Token.Asterisk(),
            '<' => new Token.LessThan(),
            '>' => new Token.GreaterThan(),
            ';' => new Token.Semicolon(),
            ',' => new Token.Comma(),
            '(' => new Token.LeftParen(),
            ')' => new Token.RightParen(),
            '{' => new Token.LeftBrace(),
            '}' => new Token.RightBrace(),
            '\0' => new Token.Eof(),
            _ => new Token.Illegal(_ch),
        };

        if (token is Token.NotEqual or Token.Equal) ReadChar();

        ReadChar();
        return token;
    }

    private void ReadChar()
    {
        if (_readPostion >= _input.Length)
        {
            _ch = '\0';
        }
        else
        {
            _ch = _input[_readPostion];
        }
        _position = _readPostion;
        _readPostion++;
    }

    private char PeekChar()
    {
        if (_readPostion >= _input.Length)
        {
            return '\0';
        }
        else
        {
            return _input[_readPostion];
        }
    }

    private string ReadIdentifier()
    {
        int position = _position;

        while (char.IsAsciiLetterOrDigit(_ch))
        {
            ReadChar();
        }

        return _input[position.._position];
    }

    private string ReadInteger()
    {
        int position = _position;

        while (char.IsAsciiDigit(_ch))
        {
            ReadChar();
        }

        return _input[position.._position];
    }

    private void SkipWhitespace()
    {
        while (char.IsWhiteSpace(_ch))
        {
            ReadChar();
        }
    }
}
