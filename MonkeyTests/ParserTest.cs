namespace MonkeyTests;

using Xunit;

using Monkey.Lexing;
using Monkey.Parsing;

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
        Assert.False(
            parser.Errors.Any(), 
            string.Join("\n".PadRight(4), parser.Errors)
        );
        Assert.NotNull(program);
        Assert.Single(program.Statements);
        
        IStatement statement = program.Statements[0];
        LetStatement letStatement = Assert.IsType<LetStatement>(statement);
        
        Assert.Equal(TokenType.Let, letStatement.Token.Type);
        Assert.Equal("let", statement.TokenLiteral());
        Assert.Equal(TokenType.Ident, letStatement.Name.Token.Type);
        Assert.Equal(expectedIdentifier, letStatement.Name.Value);
        Assert.Equal(expectedIdentifier, letStatement.Name.TokenLiteral());
    }
}
