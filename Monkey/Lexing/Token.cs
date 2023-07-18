namespace Monkey.Lexing;

public abstract record Token(string Literal)
{
    public sealed override string ToString()
    {
        return this switch
        {
            IdentToken
            or IntToken 
            or IllegalToken => $"{GetType().Name}({Literal})",
            _ => $"{GetType().Name}",
        };
    }
}

public record IdentToken(string Literal) : Token(Literal);
public record IntToken(string Literal) : Token(Literal);
public record IllegalToken(char Ch) : Token(Ch.ToString());
public record EofToken() : Token("");
public record EqualToken() : Token("==");
public record NotEqualToken() : Token("!=");
public record AssignToken() : Token("=");
public record PlusToken() : Token("+");
public record MinusToken() : Token("-");
public record BangToken() : Token("!");
public record AsteriskToken() : Token("*");
public record SlashToken() : Token("/");
public record LessThanToken() : Token("<");
public record GreaterThanToken() : Token(">");
public record CommaToken() : Token(",");
public record SemicolonToken() : Token(";");
public record LeftParenToken() : Token("(");
public record RightParenToken() : Token(")");
public record LeftBraceToken() : Token("{");
public record RightBraceToken() : Token("}");
public record FunctionToken() : Token("fn");
public record LetToken() : Token("let");
public record IfToken() : Token("if");
public record ElseToken() : Token("else");
public record ReturnToken() : Token("return");
public record TrueToken() : Token("true");
public record FalseToken() : Token("false");
