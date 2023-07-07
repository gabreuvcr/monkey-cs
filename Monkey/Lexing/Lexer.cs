namespace Monkey.Lexing;

    public class Lexer
    {
        private readonly string _input;
        private int _currentPosition;
        private int _peekPostion;
        private char _currentChar;

    public Lexer(string input)
    {
        _input = input;
        ReadChar();
    }

    public Token NextToken()
    {
        SkipWhitespace();

        if (char.IsAsciiDigit(_currentChar)) 
        {
            return Token.Int(ReadInteger());
        }
        
        if (char.IsAsciiLetter(_currentChar) || _currentChar == '_')
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

        Token token = _currentChar switch
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
            _ => Token.Illegal(_currentChar),
        };
        
        ReadChar();
        return token;
    }

    private void ReadChar()
    {
        if (_peekPostion >= _input.Length)
        {
            _currentChar = '\0';
        }
        else
        {
            _currentChar = _input[_peekPostion];
        }
        _currentPosition = _peekPostion;
        _peekPostion++;
    }

    private char PeekChar()
    {
        if (_peekPostion >= _input.Length)
        {
            return '\0';
        }
        else
        {
            return _input[_peekPostion];
        }
    }

    private string ReadIdentifier()
    {
        int position = _currentPosition;

        while (char.IsAsciiLetterOrDigit(_currentChar) || _currentChar == '_')
        {
            ReadChar();
        }

        return _input[position.._currentPosition];
    }

    private string ReadInteger()
    {
        int position = _currentPosition;

        while (char.IsAsciiDigit(_currentChar))
        {
            ReadChar();
        }

        return _input[position.._currentPosition];
    }

    private void SkipWhitespace()
    {
        while (char.IsWhiteSpace(_currentChar))
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
