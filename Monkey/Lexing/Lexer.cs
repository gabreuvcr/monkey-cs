namespace Monkey.Lexing;

public class Lexer
{
    private readonly string _input;
    private int _currentPosition;
    private int _peekPosition;
    private char _currentChar;

    public Lexer(string input)
    {
        _input = input;
        ReadChar();
    }

    public List<Token> TokenizeProgram()
    {
        List<Token> tokens = new();

        while (_currentChar is not '\0') 
        {
            tokens.Add(NextToken());
        }
        tokens.Add(NextToken());

        return tokens;
    }

    public Token NextToken()
    {
        SkipWhitespace();

        if (char.IsAsciiDigit(_currentChar)) 
        {
            return new Token.Int(ReadInteger());
        }
        
        if (char.IsAsciiLetter(_currentChar) || _currentChar == '_')
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

        Token token = _currentChar switch
        {
            '=' => PeekChar() switch
            {
                '=' => SkipPeek(new Token.Equal()),
                _ => new Token.Assign(),
            },
            '!' => PeekChar() switch
            {
                '=' => SkipPeek(new Token.NotEqual()),
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
            _ => new Token.Illegal(_currentChar),
        };
        
        ReadChar();
        return token;
    }

    private void ReadChar()
    {
        if (_peekPosition < _input.Length)
        {
            _currentChar = _input[_peekPosition];
        }
        else
        {
            _currentChar = '\0';
        }
        
        _currentPosition = _peekPosition;
        _peekPosition++;
    }

    private char PeekChar()
    {
        if (_peekPosition < _input.Length)
        {
            return _input[_peekPosition];
        }
        else
        {
            return '\0';
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
