using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;

public class DeleteTextCommand : IDocumentCommand
{
    private readonly Document _document;
    private readonly int _startIndex;
    private readonly int _length;
    private string _deletedText;

    public DeleteTextCommand(Document document, int startIndex, int length)
    {
        _document = document ?? throw new ArgumentNullException(nameof(document));
        _startIndex = startIndex;
        _length = length;
    }

    public void Execute()
    {
        if (_startIndex < 0 || _startIndex + _length > _document.Content.Length)
        {
            throw new ArgumentOutOfRangeException("Invalid range for deleting text.");
        }

        _deletedText = _document.Content.Substring(_startIndex, _length);
        _document.Edit(_document.Content.Remove(_startIndex, _length));
    }

    public void Unexecute()
    {
        _document.Edit(_document.Content.Insert(_startIndex, _deletedText));
    }
    
    public string GetDescription()
    {
        return $"Редактирование документа \"{_document.Name}\": удаление '{_deletedText}'";
    }
}