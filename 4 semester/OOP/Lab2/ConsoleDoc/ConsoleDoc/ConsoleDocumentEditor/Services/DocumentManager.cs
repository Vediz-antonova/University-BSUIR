using ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Enums;
using ConsoleDoc.ConsoleDocumentEditor.Adapters;
using ConsoleDoc.ConsoleDocumentEditor.Storage;
using System.Text.Json;
namespace ConsoleDoc.ConsoleDocumentEditor.Services;

public class DocumentManager(AdapterFactory adapterFactory, IStorage storage)
{
    private readonly List<Document?> _documents = new();
    private readonly AdapterFactory _adapterFactory = adapterFactory ?? throw new ArgumentNullException(nameof(adapterFactory));
    private readonly UndoRedoManager _undoRedoManager = new();
    private readonly IStorage _storage = storage ?? throw new ArgumentNullException(nameof(storage));

    private Document CreateDocumentByType(DocumentType type, string name, string creatorUsername, string content = "")
    {
        Document document = type switch
        {
            DocumentType.PlainText => new PlainTextDocument(name),
            DocumentType.Markdown  => new MarkdownDocument(name),
            DocumentType.RichText  => new RichTextDocument(name),
            DocumentType.Json      => new PlainTextDocument(name),
            DocumentType.Xml       => new PlainTextDocument(name),
            _ => throw new ArgumentException("Invalid document type", nameof(type))
        };
        document.Content = content;
        document.Type = type;
        document.Permissions[creatorUsername] = UserRole.Administrator;
        return document;
    }
    public Document CreateDocument(DocumentType type, string name, string creatorUsername)
    {
        var document = CreateDocumentByType(type, name, creatorUsername);
        _documents.Add(document);
        Console.WriteLine($"Document '{name}' of type {type} created by {creatorUsername}.");
        document.creatorName = creatorUsername;
        return document;
    }
    public void DeleteDocument(Document? document)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));

        if (_documents.Remove(document))
        {
            Console.WriteLine($"Document '{document.Name}' deleted.");

            if (_storage is CloudStorage cloudStorage)
            {
                if (!DocumentFormatMapping.TryGetValue(document.Type, out var format))
                    throw new ArgumentException("Unsupported document type", nameof(document.Type));

                var fileName = $"{document.Name}.{format}";
                cloudStorage.DeleteDocument(fileName);
            }
            else
            {
                _storage.DeleteDocument(document.Name);
            }
        }
        else
        {
            Console.WriteLine($"Document '{document.Name}' not found.");
        }
    }
    public List<Document?> ListDocuments()
    {
        return new List<Document?>(_documents);
    }
    public Dictionary<string, UserRole> ListUsersForDocument(Document document) {
        if (document == null)
            throw new ArgumentNullException(nameof(document));
        return new Dictionary<string, UserRole>(document.Permissions);
    }
    public bool ChangeDocumentRole(Document doc, string targetUsername, UserRole newRole, string currentUsername) {
        if (doc.Permissions[currentUsername] != UserRole.Administrator)
        {
            Console.WriteLine("Только администратор может изменять роли.");
            return false;
        }
        if (targetUsername == doc.creatorName)
        {
            Console.WriteLine("Нельзя изменить роль создателя документа.");
            return false;
        }
        doc.AssignRole(targetUsername, newRole);
        return true;
    }

    public UserRole? GetUserRole(Document doc, string username)
    {
        return doc.Permissions.ContainsKey(username) ? doc.Permissions[username] : null;
    }
    private string GetPermissionsFilePath(string documentName) => $"{documentName}_roles.json";
    
    private static readonly Dictionary<DocumentType, string> DocumentFormatMapping = new()
    {
        { DocumentType.PlainText, "txt" },
        { DocumentType.Markdown,  "md" },
        { DocumentType.RichText,  "rtf" },
        { DocumentType.Json,      "json" },
        { DocumentType.Xml,       "xml" }
    };
    public void SaveDocument(Document document)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));

        if (!DocumentFormatMapping.TryGetValue(document.Type, out var format))
            throw new ArgumentException("Unsupported document type", nameof(document.Type));

        var fileName = $"{document.Name}.{format}";
        var adapter = _adapterFactory.CreateAdapter(format);

        try
        {
            adapter.Save(document, fileName);
            _storage.SaveDocument(document.Name, document.Content, format);
            Console.WriteLine($"Document saved successfully in format {format}.");

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(document.Permissions, options);
            File.WriteAllText(GetPermissionsFilePath(document.Name), json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving document: {ex.Message}");
        }
    }
    public Document LoadDocument(string fileName, string format, DocumentType type, string username)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
        if (string.IsNullOrWhiteSpace(format))
            throw new ArgumentException("Format cannot be null or empty.", nameof(format));

        var adapter = _adapterFactory.CreateAdapter(format);
        var loadedDocument = adapter.Load($"{fileName}.{format}");

        if (loadedDocument == null)
        {
            throw new Exception($"Failed to load document from {fileName}.{format}.");
        }

        var document = CreateDocumentByType(type, fileName, loadedDocument.Content);
        Console.WriteLine($"Document loaded from {fileName}.{format} as {type}.");
        _documents.Add(document);
        _storage.LoadDocument($"{fileName}.{format}");

        string permissionsFile = GetPermissionsFilePath(fileName);
        if (File.Exists(permissionsFile))
        {
            var json = File.ReadAllText(permissionsFile);
            document.Permissions = !string.IsNullOrWhiteSpace(json)
                ? JsonSerializer.Deserialize<Dictionary<string, UserRole>>(json) ?? new Dictionary<string, UserRole>()
                : new Dictionary<string, UserRole>();
        }
        else
        {
            document.Permissions = new Dictionary<string, UserRole>();
        }

        if (!document.Permissions.ContainsKey(username))
        {
            document.Permissions[username] = UserRole.Viewer;
        }

        return document;
    }

    public void ExecuteCommand(IDocumentCommand command) =>
        _undoRedoManager.ExecuteCommand(command);
    public void Undo() => _undoRedoManager.Undo();
    public void Redo() => _undoRedoManager.Redo();
}