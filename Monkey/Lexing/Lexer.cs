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
            return Token.Int(ReadInteger());
        }
        
        if (char.IsAsciiLetter(CurrentChar) || CurrentChar == '_')
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

        var token = CurrentChar switch
        {
            '=' => PeekChar switch
            {
                '=' => SkipPeek(Token.Equal),
                _ => Token.Assign,
            },
            '!' => PeekChar switch
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
            _ => Token.Illegal(CurrentChar),
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
