namespace MonkeyTests;

using Monkey.Lexing;
using Monkey.Parsing;
using Xunit.Abstractions;
using Xunit.Sdk;

public class ParserTest
{
    [Theory]
    [InlineData("let x = 5;", "x")]
    [InlineData("let y = 10;", "y")]
    [InlineData("let foobar = 838383;", "foobar")]
    public void ParsingLetStatement(string input, string expectedIdentifier)
    {
        Lexer lexer = new(input);
        Parser parser = new(lexer);

        Ast program = parser.ParseProgram();
        Assert.Equal(new List<string>(), parser.Errors);
        Assert.Empty(parser.Errors);

        Assert.NotNull(program);
        Assert.Single(program.Statements);
        
        IStatement statement = program.Statements[0];
        Assert.Equal("let", statement.TokenLiteral());
        
        LetStatement letStatement = Assert.IsType<LetStatement>(statement);
        Assert.Equal(TokenType.Let, letStatement.Token.Type);
        Assert.Equal(expectedIdentifier, letStatement.Name.Value);
        Assert.Equal(expectedIdentifier, letStatement.Name.TokenLiteral());
    }
}
