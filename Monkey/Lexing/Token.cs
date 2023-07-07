namespace Monkey.Lexing;

public enum TokenType {
    Ident,
    Int,
    Illegal,
    Eof,
    Equal,
    NotEqual,
    Assign,
    Plus,
    Minus,
    Bang,
    Asterisk,
    Slash,
    LessThan,
    GreaterThan,
    Comma,
    Semicolon,
    LeftParen,
    RightParen,
    LeftBrace,
    RightBrace,
    Function,
    Let,
    If,
    Else,
    Return,
    True,
    False,

    Empty,
}

public record Token(TokenType Type, string Literal)
{
    public static Token Ident(string literal) => new(TokenType.Ident, literal);
    public static Token Int(string literal) => new(TokenType.Int, literal);
    public static Token Illegal(char ch) => new(TokenType.Illegal, ch.ToString());
    public static Token Eof => new(TokenType.Eof, string.Empty);
    public static Token Equal => new(TokenType.Equal, "==");
    public static Token NotEqual => new(TokenType.NotEqual, "!=");
    public static Token Assign => new(TokenType.Assign, "=");
    public static Token Plus => new(TokenType.Plus, "+");
    public static Token Minus => new(TokenType.Minus, "-");
    public static Token Bang => new(TokenType.Bang, "!");
    public static Token Asterisk => new(TokenType.Asterisk, "*");
    public static Token Slash => new(TokenType.Slash, "/");
    public static Token LessThan => new(TokenType.LessThan, "<");
    public static Token GreaterThan => new(TokenType.GreaterThan, ">");
    public static Token Comma => new(TokenType.Comma, ",");
    public static Token Semicolon => new(TokenType.Semicolon, ";");
    public static Token LeftParen => new(TokenType.LeftParen, "(");
    public static Token RightParen => new(TokenType.RightParen, ")");
    public static Token LeftBrace => new(TokenType.LeftBrace, "{");
    public static Token RightBrace => new(TokenType.RightBrace, "}");
    public static Token Function => new(TokenType.Function, "fn");
    public static Token Let => new(TokenType.Let, "let");
    public static Token If => new(TokenType.If, "if");
    public static Token Else => new(TokenType.Else, "else");
    public static Token Return => new(TokenType.Return, "return");
    public static Token True => new(TokenType.True, "true");
    public static Token False => new(TokenType.False, "false");

    public override string ToString() {
        return Type switch
        {
            TokenType.Ident or 
            TokenType.Int   or 
            TokenType.Illegal => $"{Type}({Literal})",
            _ => $"{Type}"
        };
    }
}
