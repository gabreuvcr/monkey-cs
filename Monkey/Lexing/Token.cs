namespace Monkey.Lexing;

public readonly record struct Token(TokenType Type, string Literal)
{
    public static readonly Token Equal = new Token(TokenType.Equal, "==");
    public static readonly Token NotEqual = new Token(TokenType.NotEqual, "!=");
    public static readonly Token Assign = new Token(TokenType.Assign, "=");
    public static readonly Token Plus = new Token(TokenType.Plus, "+");
    public static readonly Token Minus = new Token(TokenType.Minus, "-");
    public static readonly Token Bang = new Token(TokenType.Bang, "!");
    public static readonly Token Asterisk = new Token(TokenType.Asterisk, "*");
    public static readonly Token Slash = new Token(TokenType.Slash, "/");
    public static readonly Token LessThan = new Token(TokenType.LessThan, "<");
    public static readonly Token GreaterThan = new Token(TokenType.GreaterThan, ">");
    public static readonly Token Comma = new Token(TokenType.Comma, ",");
    public static readonly Token Semicolon = new Token(TokenType.Semicolon, ";");
    public static readonly Token LeftParen = new Token(TokenType.LeftParen, "(");
    public static readonly Token RightParen = new Token(TokenType.RightParen, ")");
    public static readonly Token LeftBrace = new Token(TokenType.LeftBrace, "{");
    public static readonly Token RightBrace = new Token(TokenType.RightBrace, "}");
    public static readonly Token Function = new Token(TokenType.Function, "fn");
    public static readonly Token Let = new Token(TokenType.Let, "let");
    public static readonly Token If = new Token(TokenType.If, "if");
    public static readonly Token Else = new Token(TokenType.Else, "else");
    public static readonly Token Return = new Token(TokenType.Return, "return");
    public static readonly Token True = new Token(TokenType.True, "true");
    public static readonly Token False = new Token(TokenType.False, "false");
    public static readonly Token Eof = new Token(TokenType.Eof, string.Empty);

    public static Token Ident(string literal) => new Token(TokenType.Ident, literal);
    public static Token Int(string literal) => new Token(TokenType.Int, literal);
    public static Token Illegal(char ch) => new Token(TokenType.Illegal, ch.ToString());

    public override readonly string ToString() {
        return $"{Type}" + 
        (
            this.Type is TokenType.Ident or TokenType.Int or TokenType.Illegal
            ? $"({Literal})" 
            : string.Empty
        );
    }
}
