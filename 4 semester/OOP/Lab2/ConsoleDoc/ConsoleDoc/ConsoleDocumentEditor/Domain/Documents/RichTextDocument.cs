namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;

public class RichTextDocument(string name) : Document(name)
{
    public override void Open()
    {
        Console.WriteLine($"Opening rich text document: {Name}");
    }
    public override void Save()
    {
        Console.WriteLine($"Saving rich text document: {Name}");
        base.Save();
    }
}