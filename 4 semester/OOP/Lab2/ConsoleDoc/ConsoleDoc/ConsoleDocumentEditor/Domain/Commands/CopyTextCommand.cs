using System.Text;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
using ConsoleDoc.ConsoleDocumentEditor.Services;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;

public class CopyTextCommand : IDocumentCommand
{
    private readonly Document _document;
    private readonly int _startIndex;
    private readonly int _length;
    private readonly Clipboard _clipboard; // Буфер обмена

    public CopyTextCommand(Document document, int startIndex, int length, Clipboard clipboard)
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
            throw new ArgumentOutOfRangeException("Invalid range for copying text.");
        }

        var copiedText = _document.Content.Substring(_startIndex, _length);
        _clipboard.Store(copiedText);
        Console.WriteLine($"Text copied: {copiedText}");
    }

    public void Unexecute()
    {
        Console.WriteLine("Undo is not applicable for copying text.");
    }
    
    public string GetDescription()
    {
        return $"Копирование в \"{_document.Name}\"";
    }
}