using System.Xml.Serialization;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Adapters;

public class XmlAdapter : IDocumentAdapter
{
    public void Save(Document document, string filePath)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        try
        {
            var xmlSerializer = new XmlSerializer(document.GetType());
            using (var writer = new StreamWriter(filePath))
            {
                xmlSerializer.Serialize(writer, document);
                Console.WriteLine($"XML Document saved to {filePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving XML Document: {ex.Message}");
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
            var xmlSerializer = new XmlSerializer(typeof(PlainTextDocument));
            using (var reader = new StreamReader(filePath))
            {
                var document = (PlainTextDocument)xmlSerializer.Deserialize(reader);
                return document;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading XML Document: {ex.Message}");
            throw;
        }
    }
}