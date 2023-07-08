namespace MonkeyTests;

using Xunit;

using Monkey.Lexing;
using Monkey.Parsing;

public class ParserTest
{
    [Fact]
    public void ParsingLetStatement()
    {
        string input = @"
            let x = 5;
            let y = 10;
            let foobar = 838383;
        ";

        List<string> expectedIdentifiers = new() {
            "x", "y", "foobar"
        };

        Lexer lexer = new(input);
        List<Token> tokens = lexer.TokenizeProgram();
        Parser parser = new(tokens);

        Ast program = parser.ParseProgram();
        Assert.NotNull(program);
        Assert.False(
            parser.Errors.Any(), 
            string.Join("\n".PadRight(4), parser.Errors)
        );
        Assert.Equal(3, program.Statements.Count());
        
        int i = 0;
        foreach (string expectedIdentifier in expectedIdentifiers)
        {
            IStatement statement = program.Statements[i];
            LetStatement letStatement = Assert.IsType<LetStatement>(statement);
            Assert.IsType<Token.Let>(letStatement.Token);
            Assert.IsType<Token.Ident>(letStatement.Name.Token);
            Assert.Equal(expectedIdentifier, letStatement.Name.Value);
            Assert.Equal(expectedIdentifier, letStatement.Name.TokenLiteral());
            i++;
        }
    }
}
