using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;

public class EditTextCommand : IDocumentCommand
{
    private readonly Document _document;
    private readonly string _newText;
    private string _previousText;

    public EditTextCommand(Document document, string newText)
    {
        _document = document ?? throw new ArgumentNullException(nameof(document));
        _newText = newText ?? throw new ArgumentNullException(nameof(newText));
        _previousText = _document.Content;
    }

    public void Execute()
    {
        _previousText = _document.Content;
        _document.Edit(_newText);
    }

    public void Unexecute()
    {
        _document.Edit(_previousText);
    }
    
    public string GetDescription()
    {
        return $"Редактирование документа \"{_document.Name}\": изменение на '{_newText}'";
    }
}