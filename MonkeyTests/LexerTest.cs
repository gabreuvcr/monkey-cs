namespace MonkeyTests;

using Monkey.Lexing;

public class LexerTests
{
    [Fact]
    public void NextToken()
    {
        string input = @"let five = 5;
            let ten = 10;

            let add = fn(x, y) {
                x + y;
            };

            let result = add(five, ten);
            !-/*5;
            5 < 10 > 5;

            if (5 < 10) {
                return true;
            } else {
                return false;
            }

            10 == 10;
            10 != 9;
        ";

        List<Token> expectedTokens = new List<Token>()
        {
            new Token.Let(),
            new Token.Ident("five"),
            new Token.Assign(),
            new Token.Int("5"),
            new Token.Semicolon(),

            new Token.Let(),
            new Token.Ident("ten"),
            new Token.Assign(),
            new Token.Int("10"),
            new Token.Semicolon(),

            new Token.Let(),
            new Token.Ident("add"),
            new Token.Assign(),
            new Token.Function(),
            new Token.LeftParen(),
            new Token.Ident("x"),
            new Token.Comma(),
            new Token.Ident("y"),
            new Token.RightParen(),
            new Token.LeftBrace(),
            new Token.Ident("x"),
            new Token.Plus(),
            new Token.Ident("y"),
            new Token.Semicolon(),
            new Token.RightBrace(),
            new Token.Semicolon(),

            new Token.Let(),
            new Token.Ident("result"),
            new Token.Assign(),
            new Token.Ident("add"),
            new Token.LeftParen(),
            new Token.Ident("five"),
            new Token.Comma(),
            new Token.Ident("ten"),
            new Token.RightParen(),
            new Token.Semicolon(),

            new Token.Bang(),
            new Token.Minus(),
            new Token.Slash(),
            new Token.Asterisk(),
            new Token.Int("5"),
            new Token.Semicolon(),

            new Token.Int("5"),
            new Token.LessThan(),
            new Token.Int("10"),
            new Token.GreaterThan(),
            new Token.Int("5"),
            new Token.Semicolon(),

            new Token.If(),
            new Token.LeftParen(),
            new Token.Int("5"),
            new Token.LessThan(),
            new Token.Int("10"),
            new Token.RightParen(),
            new Token.LeftBrace(),
            new Token.Return(),
            new Token.True(),
            new Token.Semicolon(),
            new Token.RightBrace(),
            new Token.Else(),
            new Token.LeftBrace(),
            new Token.Return(),
            new Token.False(),
            new Token.Semicolon(),
            new Token.RightBrace(),

            new Token.Int("10"),
            new Token.Equal(),
            new Token.Int("10"),
            new Token.Semicolon(),

            new Token.Int("10"),
            new Token.NotEqual(),
            new Token.Int("9"),
            new Token.Semicolon(),
            new Token.Eof(),
        };

        Lexer lexer = new Lexer(input);

        foreach (Token expectedToken in expectedTokens)
        {
            Token token = lexer.NextToken();
            Assert.Equal(expectedToken, token);
        }

    }
}