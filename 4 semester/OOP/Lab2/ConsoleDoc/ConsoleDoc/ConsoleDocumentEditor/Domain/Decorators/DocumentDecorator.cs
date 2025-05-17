using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Decorators;

public class DocumentDecorator : Document
{
    protected readonly Document InnerDocument;

    protected DocumentDecorator(Document innerDocument) : base(innerDocument.Name)
    {
        InnerDocument = innerDocument ?? throw new ArgumentNullException(nameof(innerDocument));
    }

    public override void Open() => InnerDocument.Open();
    public override void Save() => InnerDocument.Save();
    public override void Edit(string newContent) => InnerDocument.Edit(newContent);
    public override void Format() => InnerDocument.Format();
}