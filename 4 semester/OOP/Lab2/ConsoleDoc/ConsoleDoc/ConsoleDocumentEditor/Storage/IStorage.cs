namespace ConsoleDoc.ConsoleDocumentEditor.Storage;

public interface IStorage
{
    void SaveDocument(string fileName, string content, string format);
    string? LoadDocument(string fileName);
    List<string> ListDocuments();
    void DeleteDocument(string fileName);
}