using ConsoleDoc.ConsoleDocumentEditor.Domain.Models;
namespace ConsoleDoc.ConsoleDocumentEditor.History;

public class DocumentHistory : IDocumentHistory
{
    private readonly List<DocumentVersion> _versions = new();

    public void SaveVersion(string content)
    {
        var version = new DocumentVersion(content);
        _versions.Add(version);
    }
    public IEnumerable<DocumentVersion> GetVersions()
    {
        return _versions;
    }
}