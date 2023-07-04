namespace Monkey.Lexing;

public abstract record Token
{
    public record Ident(string Literal) : Token;
    public record Int(string Literal) : Token;

    public record Equal : Token;
    public record NotEqual : Token;

    public record Assign : Token;
    public record Plus : Token;
    public record Minus : Token;
    public record Bang : Token;
    public record Asterisk : Token;
    public record Slash : Token;
    public record LessThan : Token;
    public record GreaterThan : Token;

    public record Comma : Token;
    public record Semicolon : Token;

    public record LeftParen : Token;
    public record RightParen : Token;
    public record LeftBrace : Token;
    public record RightBrace : Token;

    public record Function : Token;
    public record Let : Token;
    public record If : Token;
    public record Else : Token;
    public record Return : Token;
    public record True : Token;
    public record False : Token;

    public record Illegal(char Character) : Token;
    public record Eof : Token;
}
