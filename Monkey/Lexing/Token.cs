namespace Monkey.Lexing;

public readonly record struct Token(TokenType Type, string Literal)
{
    public static readonly Token Equal = new(TokenType.Equal, "==");
    public static readonly Token NotEqual = new(TokenType.NotEqual, "!=");
    public static readonly Token Assign = new(TokenType.Assign, "=");
    public static readonly Token Plus = new(TokenType.Plus, "+");
    public static readonly Token Minus = new(TokenType.Minus, "-");
    public static readonly Token Bang = new(TokenType.Bang, "!");
    public static readonly Token Asterisk = new(TokenType.Asterisk, "*");
    public static readonly Token Slash = new(TokenType.Slash, "/");
    public static readonly Token LessThan = new(TokenType.LessThan, "<");
    public static readonly Token GreaterThan = new(TokenType.GreaterThan, ">");
    public static readonly Token Comma = new(TokenType.Comma, ",");
    public static readonly Token Semicolon = new(TokenType.Semicolon, ";");
    public static readonly Token LeftParen = new(TokenType.LeftParen, "(");
    public static readonly Token RightParen = new(TokenType.RightParen, ")");
    public static readonly Token LeftBrace = new(TokenType.LeftBrace, "{");
    public static readonly Token RightBrace = new(TokenType.RightBrace, "}");
    public static readonly Token Function = new(TokenType.Function, "fn");
    public static readonly Token Let = new(TokenType.Let, "let");
    public static readonly Token If = new(TokenType.If, "if");
    public static readonly Token Else = new(TokenType.Else, "else");
    public static readonly Token Return = new(TokenType.Return, "return");
    public static readonly Token True = new(TokenType.True, "true");
    public static readonly Token False = new(TokenType.False, "false");
    public static readonly Token Eof = new(TokenType.Eof, string.Empty);

    public static Token Ident(string literal) => new(TokenType.Ident, literal);
    public static Token Int(string literal) => new(TokenType.Int, literal);
    public static Token Illegal(char ch) => new(TokenType.Illegal, ch.ToString());

        public override readonly string ToString() {
        return $"{Type}" + Type switch
        {
            TokenType.Ident or
            TokenType.Int or
            TokenType.Illegal => $"({Literal})",
            _ => string.Empty
        };
    }
}
