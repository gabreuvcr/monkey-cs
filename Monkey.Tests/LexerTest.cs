namespace Monkey.Tests;

using Xunit;

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

        List<Token> expectedTokens = new()
        {
            new LetToken(),
            new IdentToken("five"),
            new AssignToken(),
            new IntToken("5"),
            new SemicolonToken(),

            new LetToken(),
            new IdentToken("ten"),
            new AssignToken(),
            new IntToken("10"),
            new SemicolonToken(),

            new LetToken(),
            new IdentToken("add"),
            new AssignToken(),
            new FunctionToken(),
            new LeftParenToken(),
            new IdentToken("x"),
            new CommaToken(),
            new IdentToken("y"),
            new RightParenToken(),
            new LeftBraceToken(),
            new IdentToken("x"),
            new PlusToken(),
            new IdentToken("y"),
            new SemicolonToken(),
            new RightBraceToken(),
            new SemicolonToken(),

            new LetToken(),
            new IdentToken("result"),
            new AssignToken(),
            new IdentToken("add"),
            new LeftParenToken(),
            new IdentToken("five"),
            new CommaToken(),
            new IdentToken("ten"),
            new RightParenToken(),
            new SemicolonToken(),

            new BangToken(),
            new MinusToken(),
            new SlashToken(),
            new AsteriskToken(),
            new IntToken("5"),
            new SemicolonToken(),

            new IntToken("5"),
            new LessThanToken(),
            new IntToken("10"),
            new GreaterThanToken(),
            new IntToken("5"),
            new SemicolonToken(),

            new IfToken(),
            new LeftParenToken(),
            new IntToken("5"),
            new LessThanToken(),
            new IntToken("10"),
            new RightParenToken(),
            new LeftBraceToken(),
            new ReturnToken(),
            new TrueToken(),
            new SemicolonToken(),
            new RightBraceToken(),
            new ElseToken(),
            new LeftBraceToken(),
            new ReturnToken(),
            new FalseToken(),
            new SemicolonToken(),
            new RightBraceToken(),

            new IntToken("10"),
            new EqualToken(),
            new IntToken("10"),
            new SemicolonToken(),

            new IntToken("10"),
            new NotEqualToken(),
            new IntToken("9"),
            new SemicolonToken(),
            new EofToken(),
        };

        Lexer lexer = new(input);

        foreach (Token expectedToken in expectedTokens)
        {
            Token token = lexer.NextToken();
            Assert.Equal(expectedToken, token);
        }

    }
}