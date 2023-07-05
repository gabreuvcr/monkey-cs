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
            return Token.Int(ReadInteger());
        }
        
        if (char.IsAsciiLetter(_ch) || _ch == '_')
        {
            return ReadIdentifier() switch
            {
                "fn" => Token.Function,
                "let" => Token.Let,
                "if" => Token.If,
                "else" => Token.Else,
                "return" => Token.Return,
                "true" => Token.True,
                "false" => Token.False,
                string ident => Token.Ident(ident),
            };
        }

        Token token = _ch switch
        {
            '=' => PeekChar() switch
            {
                '=' => SkipPeek(Token.Equal),
                _ => Token.Assign,
            },
            '!' => PeekChar() switch
            {
                '=' => SkipPeek(Token.NotEqual),
                _ => Token.Bang,
            },
            '-' => Token.Minus,
            '+' => Token.Plus,
            '/' => Token.Slash,
            '*' => Token.Asterisk,
            '<' => Token.LessThan,
            '>' => Token.GreaterThan,
            ';' => Token.Semicolon,
            ',' => Token.Comma,
            '(' => Token.LeftParen,
            ')' => Token.RightParen,
            '{' => Token.LeftBrace,
            '}' => Token.RightBrace,
            '\0' => Token.Eof,
            _ => Token.Illegal(_ch),
        };
        
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

        while (char.IsAsciiLetterOrDigit(_ch) || _ch == '_')
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

    private Token SkipPeek(Token token)
    {
        ReadChar();
        return token;
    }
}
