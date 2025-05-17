using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Decorators;

public class Heading1Decorator(Document innerDocument) : DocumentDecorator(innerDocument)
{
    public new string Content => $"# {InnerDocument.Content}";
}