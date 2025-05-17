using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Adapters;

public interface IDocumentAdapter
{
    void Save(Document document, string filePath);
    Document Load(string filePath);
}