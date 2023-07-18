namespace Monkey.Tests;

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
                    Tokens.Let,
                    new IdentifierExpression(
                        Tokens.Ident("myVar"),
                        "myVar"
                    ),
                    new IdentifierExpression(
                        Tokens.Ident("anotherVar"),
                        "anotherVar"
                    )
                )
            }  
        );

        Assert.Equal("let myVar = anotherVar;", program.ToString());
    }
}