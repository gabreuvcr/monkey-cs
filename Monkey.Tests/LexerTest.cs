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
            Tokens.Let,
            Tokens.Ident("five"),
            Tokens.Assign,
            Tokens.Int("5"),
            Tokens.Semicolon,

            Tokens.Let,
            Tokens.Ident("ten"),
            Tokens.Assign,
            Tokens.Int("10"),
            Tokens.Semicolon,

            Tokens.Let,
            Tokens.Ident("add"),
            Tokens.Assign,
            Tokens.Function,
            Tokens.LeftParen,
            Tokens.Ident("x"),
            Tokens.Comma,
            Tokens.Ident("y"),
            Tokens.RightParen,
            Tokens.LeftBrace,
            Tokens.Ident("x"),
            Tokens.Plus,
            Tokens.Ident("y"),
            Tokens.Semicolon,
            Tokens.RightBrace,
            Tokens.Semicolon,

            Tokens.Let,
            Tokens.Ident("result"),
            Tokens.Assign,
            Tokens.Ident("add"),
            Tokens.LeftParen,
            Tokens.Ident("five"),
            Tokens.Comma,
            Tokens.Ident("ten"),
            Tokens.RightParen,
            Tokens.Semicolon,

            Tokens.Bang,
            Tokens.Minus,
            Tokens.Slash,
            Tokens.Asterisk,
            Tokens.Int("5"),
            Tokens.Semicolon,

            Tokens.Int("5"),
            Tokens.LessThan,
            Tokens.Int("10"),
            Tokens.GreaterThan,
            Tokens.Int("5"),
            Tokens.Semicolon,

            Tokens.If,
            Tokens.LeftParen,
            Tokens.Int("5"),
            Tokens.LessThan,
            Tokens.Int("10"),
            Tokens.RightParen,
            Tokens.LeftBrace,
            Tokens.Return,
            Tokens.True,
            Tokens.Semicolon,
            Tokens.RightBrace,
            Tokens.Else,
            Tokens.LeftBrace,
            Tokens.Return,
            Tokens.False,
            Tokens.Semicolon,
            Tokens.RightBrace,

            Tokens.Int("10"),
            Tokens.Equal,
            Tokens.Int("10"),
            Tokens.Semicolon,

            Tokens.Int("10"),
            Tokens.NotEqual,
            Tokens.Int("9"),
            Tokens.Semicolon,
            Tokens.Eof,
        };

        Lexer lexer = new(input);

        foreach (Token expectedToken in expectedTokens)
        {
            Token token = lexer.NextToken();
            Assert.Equal(expectedToken, token);
        }

    }
}