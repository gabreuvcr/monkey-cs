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
                    new LetToken(),
                    new IdentifierExpression(
                        new IdentToken("myVar"),
                        "myVar"
                    ),
                    new IdentifierExpression(
                        new IdentToken("anotherVar"),
                        "anotherVar"
                    )
                )
            }  
        );

        Assert.Equal("let myVar = anotherVar;", program.ToString());
    }
}