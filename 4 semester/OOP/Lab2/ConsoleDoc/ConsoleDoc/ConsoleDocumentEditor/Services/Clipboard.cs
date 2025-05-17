namespace ConsoleDoc.ConsoleDocumentEditor.Services;

public class Clipboard
{
    private string _text = " ";

    public void Store(string text)
    {
        _text = text;
    }
    public string Retrieve()
    {
        return _text;
    }
}