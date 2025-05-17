namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;

public class PlainTextDocument(string name) : Document(name)
{
    public override void Open()
    {
        Console.WriteLine($"Opening plain text document: {Name}");
    }
    public override void Save()
    {
        Console.WriteLine($"Saving plain text document: {Name}");
        base.Save();
    }
}