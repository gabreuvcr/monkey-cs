namespace Monkey.Lexing;

public record Token(string Literal)
{
    public sealed override string ToString()
    {
        return this switch
        {
            Ident 
            or Int 
            or Illegal => $"{this.GetType().Name}({Literal})",
            _ => $"{this.GetType().Name}",
        };
    }
}

public record Ident(string Literal) : Token(Literal);
public record Int(string Literal) : Token(Literal);
public record Illegal(char Ch) : Token(Ch.ToString());
public record Eof() : Token("");
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

public static class Tokens
{
    public static Token Ident(string literal) => new Ident(literal);
    public static Token Int(string literal) => new Int(literal);
    public static Token Illegal(char ch) => new Illegal(ch);
    public static Token Eof = new Eof();
    public static Token Equal = new Equal();
    public static Token NotEqual = new NotEqual();
    public static Token Assign = new Assign();
    public static Token Plus = new Plus();
    public static Token Minus = new Minus();
    public static Token Bang = new Bang();
    public static Token Asterisk = new Asterisk();
    public static Token Slash = new Slash();
    public static Token LessThan = new LessThan();
    public static Token GreaterThan = new GreaterThan();
    public static Token Comma = new Comma();
    public static Token Semicolon = new Semicolon();
    public static Token LeftParen = new LeftParen();
    public static Token RightParen = new RightParen();
    public static Token LeftBrace = new LeftBrace();
    public static Token RightBrace = new RightBrace();
    public static Token Function = new Function();
    public static Token Let = new Let();
    public static Token If = new If();
    public static Token Else = new Else();
    public static Token Return = new Return();
    public static Token True = new True();
    public static Token False = new False();
}
