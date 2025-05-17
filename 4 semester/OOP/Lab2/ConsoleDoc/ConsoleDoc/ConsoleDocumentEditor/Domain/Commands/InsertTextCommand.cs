using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;

public class InsertTextCommand : IDocumentCommand
{
    private readonly Document _document;
    private readonly string _text;
    private readonly int _position;
    private string _previousContent;

    public InsertTextCommand(Document document, string text, int position)
    {
        _document = document ?? throw new ArgumentNullException(nameof(document));
        _text = text ?? throw new ArgumentNullException(nameof(text));
        _position = position;
        _previousContent = _document.Content;
    }

    public void Execute()
    {
        _previousContent = _document.Content;            
        _document.Edit(_document.Content.Insert(_position, _text));
    }

    public void Unexecute()
    {
        _document.Edit(_previousContent);
    }
    
    public string GetDescription()
    {
        return $"Редактирование документа \"{_document.Name}\": вставка текста '{_text}'";
    }
}