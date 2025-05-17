using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Adapters;

public class MdAdapter : IDocumentAdapter
{
    public void Save(Document document, string filePath)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            
        try
        {
            var markdownContent = $"# {document.Name}\n\n{document.Content}";
            File.WriteAllText(filePath, markdownContent);
            Console.WriteLine($"Markdown Document saved to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving Markdown Document: {ex.Message}");
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
            return new MarkdownDocument(name) { Content = content };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading Markdown Document: {ex.Message}");
            throw;
        }
    }
}