namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;

public class MarkdownDocument(string name) : Document(name)
{
    public override void Open()
    {
        Console.WriteLine($"Opening Markdown document: {Name}");
    }
    public override void Save()
    {
        Console.WriteLine($"Saving Markdown document: {Name}");
        base.Save();
    }
}