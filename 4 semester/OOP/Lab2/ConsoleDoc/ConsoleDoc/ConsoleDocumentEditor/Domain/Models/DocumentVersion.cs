namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Models;

public class DocumentVersion(string content)
{
    private string Content { get; } = content;
    private DateTime Timestamp { get; } = DateTime.Now;

    public override string ToString() => $"Version saved at {Timestamp}: {Content}";
}