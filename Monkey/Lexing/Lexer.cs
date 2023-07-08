namespace Monkey.Lexing;

public class Lexer
{
    private readonly string _input;
    private int _currPosition;
    private int _peekPosition;
    private char CurrChar
    {
        get => _currPosition < _input.Length ? _input[_currPosition] : '\0';
    } 
    private char PeekChar
    {
        get => _peekPosition < _input.Length ? _input[_peekPosition] : '\0';
    }

    public Lexer(string input)
    {
        _input = input;
        _currPosition = 0;
        _peekPosition = 1;
    }

    public List<Token> TokenizeProgram()
    {
        List<Token> tokens = new();

        while (CurrChar is not '\0') 
        {
            tokens.Add(NextToken());
        }
        tokens.Add(NextToken());

        return tokens;
    }

    public Token NextToken()
    {
        SkipWhitespace();

        if (char.IsAsciiDigit(CurrChar)) 
        {
            return new Token.Int(ReadInteger());
        }
        
        if (char.IsAsciiLetter(CurrChar) || CurrChar == '_')
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

        Token token = CurrChar switch
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
            _ => new Token.Illegal(CurrChar),
        };
        
        ReadChar();
        return token;
    }

    private void ReadChar()
    {        
        _currPosition = _peekPosition;
        _peekPosition++;
    }

    private string ReadIdentifier()
    {
        int position = _currPosition;
        
        while (char.IsAsciiLetterOrDigit(CurrChar) || CurrChar == '_')
        {
            ReadChar();
        }

        return _input[position.._currPosition];
    }

    private string ReadInteger()
    {
        int position = _currPosition;

        while (char.IsAsciiDigit(CurrChar))
        {
            ReadChar();
        }

        return _input[position.._currPosition];
    }

    private void SkipWhitespace()
    {
        while (char.IsWhiteSpace(CurrChar))
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
