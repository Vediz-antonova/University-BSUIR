using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Adapters;

public class RtfAdapter : IDocumentAdapter
{
    public void Save(Document document, string filePath)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            
        try
        {
            var rtfContent = $"{{\\rtf1\\ansi\\def0 {{\\fonttbl{{\\f0\\fnil Helvetica;}}}}\n\\viewkind4\\uc1\\pard {document.Content}\\par}}";
            File.WriteAllText(filePath, rtfContent);
            Console.WriteLine($"Rich Text Document saved to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving Rich Text Document: {ex.Message}");
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
            return new RichTextDocument(name) { Content = content };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading Rich Text Document: {ex.Message}");
            throw;
        }
    }
}