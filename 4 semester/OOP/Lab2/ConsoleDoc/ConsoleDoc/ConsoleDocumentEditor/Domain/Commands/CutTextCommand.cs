using System.Text;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
using ConsoleDoc.ConsoleDocumentEditor.Services;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;

public class CutTextCommand : IDocumentCommand
{
    private readonly Document _document;
    private readonly int _startIndex;
    private readonly int _length;
    private readonly Clipboard _clipboard;
    private string _deletedText;

    public CutTextCommand(Document document, int startIndex, int length, Clipboard clipboard)
    {
        _document = document ?? throw new ArgumentNullException(nameof(document));
        _clipboard = clipboard ?? throw new ArgumentNullException(nameof(clipboard));
        _startIndex = startIndex;
        _length = length;
    }

    public void Execute()
    {
        if (_startIndex < 0 || _startIndex + _length > _document.Content.Length)
        {
            throw new ArgumentOutOfRangeException("Invalid range for cutting text.");
        }

        _deletedText = _document.Content.Substring(_startIndex, _length);
        _clipboard.Store(_deletedText);
        _document.Edit(_document.Content.Remove(_startIndex, _length));
        Console.WriteLine($"Text cut: {_deletedText}");
    }

    public void Unexecute()
    {
        _document.Edit(_document.Content.Insert(_startIndex, _deletedText));
    }
    
    public string GetDescription()
    {
        return $"Редактирование документа \"{_document.Name}\": вырезка текста '{_deletedText}'";
    }
}