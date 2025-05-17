using ConsoleDoc.ConsoleDocumentEditor.Domain.Models;
namespace ConsoleDoc.ConsoleDocumentEditor.History;

public interface IDocumentHistory
{
    void SaveVersion(string content);
    IEnumerable<DocumentVersion> GetVersions();
}