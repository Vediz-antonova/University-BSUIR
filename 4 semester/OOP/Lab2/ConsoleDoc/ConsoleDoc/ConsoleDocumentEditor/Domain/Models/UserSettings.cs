namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Models;

public class UserSettings
{
    public string Username { get; set; }
    public string ForegroundColor { get; set; } = "Gray";
    public string BackgroundColor { get; set; } = "Black";
}