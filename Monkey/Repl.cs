using Monkey.Lexing;

namespace Monkey.Repl;

public static class Repl
{
    public static void Start()
    {
        Console.WriteLine("Hello! This is the Monkey programming language!");
        Console.WriteLine("Feel free to type in commands.");
        StartLoop();
    }

    public static void StartLoop()
    {
        while (true)
        {   
            Console.Write(">> ");
            string? line = Console.ReadLine();

            if (line is null) { Console.WriteLine(); break; }

            Lexer lexer = new Lexer(line);
            
            for (Token token = lexer.NextToken(); token != Token.Eof; token = lexer.NextToken())
            {
                Console.WriteLine(token);
            }
        }
    }
}