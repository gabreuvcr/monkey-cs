namespace Monkey.Lexing;

public record Token(string Literal)
{
    public record Ident(string Literal) : Token(Literal);
    public record Int(string Literal) : Token(Literal);
    public record Illegal(char Char) : Token(Char.ToString());
    public record Eof() : Token(string.Empty);
    public record Equal() : Token("==");
    public record NotEqual() : Token("!=");
    public record Assign() : Token("=");
    public record Plus() : Token("+");
    public record Minus() : Token("-");
    public record Bang() : Token("!");
    public record Asterisk() : Token("*");
    public record Slash() : Token("/");
    public record LessThan() : Token("<");
    public record GreaterThan() : Token(">");
    public record Comma() : Token(",");
    public record Semicolon() : Token(";");
    public record LeftParen() : Token("(");
    public record RightParen() : Token(")");
    public record LeftBrace() : Token("{");
    public record RightBrace() : Token("}");
    public record Function() : Token("fn");
    public record Let() : Token("let");
    public record If() : Token("if");
    public record Else() : Token("else");
    public record Return() : Token("return");
    public record True() : Token("true");
    public record False() : Token("false");

    public sealed override string ToString() {
        return this switch
        {
            Ident or 
            Int   or 
            Illegal => $"{GetType().Name}({Literal})",
            _ => $"{GetType().Name}"
        };
    }
}
