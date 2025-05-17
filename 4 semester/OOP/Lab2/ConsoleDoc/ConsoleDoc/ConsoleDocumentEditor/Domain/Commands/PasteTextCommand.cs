using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
using ConsoleDoc.ConsoleDocumentEditor.Services;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;

public class PasteTextCommand : IDocumentCommand
{
    private readonly Document _document;
    private readonly Clipboard _clipboard;
    private readonly int _position;
    private string _previousContent;

    public PasteTextCommand(Document document, Clipboard clipboard, int position)
    {
        _document = document ?? throw new ArgumentNullException(nameof(document));
        _clipboard = clipboard ?? throw new ArgumentNullException(nameof(clipboard));
        _position = position;
        _previousContent = _document.Content;
    }

    public void Execute()
    {
        _previousContent = _document.Content;
        var textToPaste = _clipboard.Retrieve();
        _document.Edit(_document.Content.Insert(_position, textToPaste));
        Console.WriteLine($"Text pasted: {textToPaste}");
    }

    public void Unexecute()
    {
        _document.Edit(_previousContent);
    }
    
    public string GetDescription()
    {
        return $"Редактирование документа \"{_document.Name}\": вставка скопированного";
    }
}