namespace Monkey.Lexing;

public readonly record struct Token(TokenType Type, string Literal)
{
    public static Token Ident(string literal) => new(TokenType.Ident, literal);
    public static Token Int(string literal) => new(TokenType.Int, literal);
    public static Token Illegal(char ch) => new(TokenType.Illegal, ch.ToString());

    public readonly static Token Equal = new(TokenType.Equal, "==");
    public readonly static Token NotEqual = new(TokenType.NotEqual, "!=");

    public readonly static Token Assign = new(TokenType.Assign, "=");
    public readonly static Token Plus = new(TokenType.Plus, "+");
    public readonly static Token Minus = new(TokenType.Minus, "-");
    public readonly static Token Bang = new(TokenType.Bang, "!");
    public readonly static Token Asterisk = new(TokenType.Asterisk, "*");
    public readonly static Token Slash = new(TokenType.Slash, "/");
    public readonly static Token LessThan = new(TokenType.LessThan, "<");
    public readonly static Token GreaterThan = new(TokenType.GreaterThan, ">");

    public readonly static Token Comma = new(TokenType.Comma, ",");
    public readonly static Token Semicolon = new(TokenType.Semicolon, ";");

    public readonly static Token LeftParen = new(TokenType.LeftParen, "(");
    public readonly static Token RightParen = new(TokenType.RightParen, ")");
    public readonly static Token LeftBrace = new(TokenType.LeftBrace, "{");
    public readonly static Token RightBrace = new(TokenType.RightBrace, "}");

    public readonly static Token Function = new(TokenType.Function, "fn");
    public readonly static Token Let = new(TokenType.Let, "let");
    public readonly static Token If = new(TokenType.If, "if");
    public readonly static Token Else = new(TokenType.Else, "else");
    public readonly static Token Return = new(TokenType.Return, "return");
    public readonly static Token True = new(TokenType.True, "true");
    public readonly static Token False = new(TokenType.False, "false");

    public readonly static Token Eof = new(TokenType.Eof, string.Empty);

    public override string ToString() {
        return $"{Type}" + 
        (
            this.Type is TokenType.Ident or TokenType.Int or TokenType.Illegal
            ? $"({Literal})" 
            : string.Empty
        );
    }
}
