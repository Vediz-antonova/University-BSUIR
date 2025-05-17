using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;

public class FindTextCommand : IDocumentCommand
{
    private readonly Document _document;
    private readonly string _searchText;

    public FindTextCommand(Document document, string searchText)
    {
        _document = document ?? throw new ArgumentNullException(nameof(document));
        _searchText = searchText ?? throw new ArgumentNullException(nameof(searchText));
    }

    public void Execute()
    {
        var indices = new List<int>();
        for (var index = 0; index <= _document.Content.Length - _searchText.Length; index++)
        {
            if (_document.Content.Substring(index, _searchText.Length).Equals(_searchText))
            {
                indices.Add(index);
            }
        }

        if (indices.Count > 0)
        {
            Console.WriteLine($"Text '{_searchText}' found at positions: {string.Join(", ", indices)}");
        }
        else
        {
            Console.WriteLine($"Text '{_searchText}' not found in the document.");
        }
    }

    public void Unexecute()
    {
        Console.WriteLine("Undo is not applicable for searching text.");
    }
    
    public string GetDescription()
    {
        return $"Поиск в документе \"{_document.Name}\"";
    }
}