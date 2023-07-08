using Monkey.Lexing;

namespace Monkey.Repl;

public static class Repl
{
    public static void Start()
    {
        Console.CancelKeyPress += (sender, args) => Console.WriteLine();
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

            if (line is null) break;

            Lexer lexer = new(line);
            List<Token> tokens = lexer.TokenizeProgram();

            foreach (Token token in tokens) 
            {
                Console.WriteLine(token);
            }
        }

        Console.WriteLine();
    }
}
