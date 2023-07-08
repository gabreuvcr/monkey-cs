namespace Monkey.Lexing;

public class Lexer
{
    private readonly string _input;
    private int _position;
    private char CurrentChar
    {
        get => _position < _input.Length ? _input[_position] : '\0';
    } 
    private char PeekChar
    {
        get => _position + 1 < _input.Length ? _input[_position + 1] : '\0';
    }

    public Lexer(string input)
    {
        _input = input;
        _position = 0;
    }

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
            return new Token.Int(ReadInteger());
        }
        
        if (char.IsAsciiLetter(CurrentChar) || CurrentChar == '_')
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

        Token token = CurrentChar switch
        {
            '=' => PeekChar switch
            {
                '=' => SkipPeek(new Token.Equal()),
                _ => new Token.Assign(),
            },
            '!' => PeekChar switch
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
            _ => new Token.Illegal(CurrentChar),
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
