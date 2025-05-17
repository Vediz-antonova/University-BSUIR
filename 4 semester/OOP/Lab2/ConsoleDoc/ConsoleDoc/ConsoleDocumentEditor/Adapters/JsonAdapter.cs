using System.Text.Json;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Adapters;

public class JsonAdapter : IDocumentAdapter
{
    public void Save(Document document, string filePath)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        try
        {
            string json = JsonSerializer.Serialize(document);
            File.WriteAllText(filePath, json);
            Console.WriteLine($"JSON Document saved to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving JSON Document: {ex.Message}");
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
            string json = File.ReadAllText(filePath);
            var document = JsonSerializer.Deserialize<PlainTextDocument>(json);
            return document;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading JSON Document: {ex.Message}");
            throw;
        }
    }
}