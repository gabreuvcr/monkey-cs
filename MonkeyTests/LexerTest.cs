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

        List<Token> expectedTokens = new()
        {
            Token.Let,
            Token.Ident("five"),
            Token.Assign,
            Token.Int("5"),
            Token.Semicolon,

            Token.Let,
            Token.Ident("ten"),
            Token.Assign,
            Token.Int("10"),
            Token.Semicolon,

            Token.Let,
            Token.Ident("add"),
            Token.Assign,
            Token.Function,
            Token.LeftParen,
            Token.Ident("x"),
            Token.Comma,
            Token.Ident("y"),
            Token.RightParen,
            Token.LeftBrace,
            Token.Ident("x"),
            Token.Plus,
            Token.Ident("y"),
            Token.Semicolon,
            Token.RightBrace,
            Token.Semicolon,

            Token.Let,
            Token.Ident("result"),
            Token.Assign,
            Token.Ident("add"),
            Token.LeftParen,
            Token.Ident("five"),
            Token.Comma,
            Token.Ident("ten"),
            Token.RightParen,
            Token.Semicolon,

            Token.Bang,
            Token.Minus,
            Token.Slash,
            Token.Asterisk,
            Token.Int("5"),
            Token.Semicolon,

            Token.Int("5"),
            Token.LessThan,
            Token.Int("10"),
            Token.GreaterThan,
            Token.Int("5"),
            Token.Semicolon,

            Token.If,
            Token.LeftParen,
            Token.Int("5"),
            Token.LessThan,
            Token.Int("10"),
            Token.RightParen,
            Token.LeftBrace,
            Token.Return,
            Token.True,
            Token.Semicolon,
            Token.RightBrace,
            Token.Else,
            Token.LeftBrace,
            Token.Return,
            Token.False,
            Token.Semicolon,
            Token.RightBrace,

            Token.Int("10"),
            Token.Equal,
            Token.Int("10"),
            Token.Semicolon,

            Token.Int("10"),
            Token.NotEqual,
            Token.Int("9"),
            Token.Semicolon,
            Token.Eof,
        };

        Lexer lexer = new(input);

        foreach (Token expectedToken in expectedTokens)
        {
            Token token = lexer.NextToken();
            Assert.Equal(expectedToken, token);
        }

    }
}