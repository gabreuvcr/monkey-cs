namespace MonkeyTests;

using Xunit;

using Monkey.Lexing;
using Monkey.Parsing;

public class AstTests
{
    [Fact]
    public void NextToke()
    {
        Ast program = new(
            new List<IStatement>()
            {
                new LetStatement(
                    Token.Let,
                    new IdentifierExpression(
                        Token.Ident("myVar"),
                        "myVar"
                    ),
                    new IdentifierExpression(
                        Token.Ident("anotherVar"),
                        "anotherVar"
                    )
                )
            }  
        );

        Assert.Equal("let myVar = anotherVar;", program.ToString());
    }
}