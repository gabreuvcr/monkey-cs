namespace MonkeyTests;

using Xunit;

using Monkey.Lexing;
using Monkey.Parsing;

public class ParserTest
{
    [Fact]
    public void ParsingLetStatements()
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
        
        for (int i = 0; i < expectedIdentifiers.Count(); i++)
        {
            IStatement statement = program.Statements[i];
            string expectedIdentifier = expectedIdentifiers[i];

            LetStatement letStatement = Assert.IsType<LetStatement>(statement);
            Assert.IsType<Token.Let>(letStatement.Token);
            Assert.IsType<Token.Ident>(letStatement.Name.Token);
            Assert.Equal(expectedIdentifier, letStatement.Name.Value);
            Assert.Equal(expectedIdentifier, letStatement.Name.TokenLiteral());

        }
    }

    [Fact]
    public void ParsingReturnStatements()
    {
        string input = @"
            return 5;
            return 10;
            return 993322;
        ";

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
        
        foreach (IStatement statement in program.Statements)
        {
            ReturnStatement returnStatement = Assert.IsType<ReturnStatement>(statement);
            Assert.IsType<Token.Return>(returnStatement.Token);
        }
    }
}
