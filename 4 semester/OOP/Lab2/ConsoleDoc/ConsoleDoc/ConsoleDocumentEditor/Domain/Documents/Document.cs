using ConsoleDoc.ConsoleDocumentEditor.Domain.Enums;
using ConsoleDoc.ConsoleDocumentEditor.History;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Models;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;

public abstract class Document
{
    public string creatorName { get; set; }
    public Dictionary<string, UserRole> Permissions { get; set; } = new Dictionary<string, UserRole>();
    public string Name { get; set; }
    public DocumentType Type { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    private readonly DocumentHistory _history;

    protected Document(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Title cannot be null or empty.");
        }

        Name = name;
        Content = string.Empty;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        _history = new DocumentHistory();
    }
    
    public void AssignRole(string username, UserRole role) {
        Permissions[username] = role;
    }
    
    public abstract void Open();
    public virtual void Edit(string newContent)
    {
        Content = newContent;
        UpdatedAt = DateTime.Now;
    }
    public virtual void Save()
    {
        Console.WriteLine($"Document '{Name}' has been saved.");
        UpdatedAt = DateTime.Now;
        SaveVersion();
    }
    public virtual void Format()
    {
        Console.WriteLine($"Formatting document '{Name}'.");
    }

    private void SaveVersion()
    {
        _history.SaveVersion(Content);
    }
}