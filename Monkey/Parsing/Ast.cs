using System.Text;

namespace Monkey.Parsing;

public class Ast : INode
{
    public readonly List<IStatement> Statements = new();

    public Ast() { }

    public Ast(List<IStatement> statements)
    {
        Statements = statements;
    }

    public string TokenLiteral()
    {
        if (Statements.Count > 0) return Statements[0].TokenLiteral();

        return string.Empty;
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        foreach (IStatement statement in Statements)
        {
            sb.Append(statement.ToString());
        }

        return sb.ToString();
    }
}
