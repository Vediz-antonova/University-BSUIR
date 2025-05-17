namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Models;

public class Notification
{
    public string Message { get; }
    public string Originator { get; }
    public DateTime Timestamp { get; }

    public Notification(string message, string originator)
    {
        Message = message;
        Originator = originator;
        Timestamp = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Timestamp:HH:mm:ss} - {Message} (by {Originator})";
    }
}