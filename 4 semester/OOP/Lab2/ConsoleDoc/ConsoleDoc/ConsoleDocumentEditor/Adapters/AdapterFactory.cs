namespace ConsoleDoc.ConsoleDocumentEditor.Adapters;

public class AdapterFactory
{
    public IDocumentAdapter CreateAdapter(string format)
    {
        return format.ToLower() switch
        {
            "txt" => new TxtAdapter(),
            "md" => new MdAdapter(),
            "rtf" => new RtfAdapter(),
            "json" => new JsonAdapter(),
            "xml" => new XmlAdapter(),
            _ => throw new ArgumentException($"Unsupported format: {format}", nameof(format))
        };
    }
}