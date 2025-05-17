using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Decorators;

public class ItalicDecorator(Document innerDocument) : DocumentDecorator(innerDocument)
{
    public new string Content => $"*{InnerDocument.Content}*";
}