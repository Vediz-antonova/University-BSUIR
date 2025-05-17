using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Adapters;

public class TxtAdapter : IDocumentAdapter
{
    public void Save(Document document, string filePath)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        try
        {
            File.WriteAllText(filePath, $"{document.Name}\n{document.Content}");
            Console.WriteLine($"TXT Document saved to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving TXT Document: {ex.Message}");
            throw; 
        }
    }
    public Document Load(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        try
        {
            var content = File.ReadAllText(filePath);
            var name = Path.GetFileNameWithoutExtension(filePath);
            return new PlainTextDocument(name) { Content = content };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading TXT Document: {ex.Message}");
            throw;
        }
    }
}