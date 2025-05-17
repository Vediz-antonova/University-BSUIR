namespace ConsoleDoc.ConsoleDocumentEditor.Storage;

public class LocalStorage : IStorage
{
    public string FolderPath { get; }

    public LocalStorage(string folderPath)
    {
        FolderPath = folderPath;

        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }
    }

    public void SaveDocument(string fileName, string content, string format)
    {
        var filePath = Path.Combine(FolderPath, fileName + "." + format);
        try
        {
            File.WriteAllText(filePath, content);
            Console.WriteLine($"Document saved locally as '{filePath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving document locally: {ex.Message}");
        }
    }
    public string? LoadDocument(string fileName)
    {
        var filePath = Path.Combine(FolderPath, fileName);
        Console.WriteLine($"Loading document from local file '{filePath}'.");
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File '{fileName}' does not exist.");
                return null;
            }

            return File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading document locally: {ex.Message}");
            return null;
        }
    }
    public List<string> ListDocuments()
    {
        var documents = new List<string>();
        try
        {
            if (Directory.Exists(FolderPath))
            {
                string[] files = Directory.GetFiles(FolderPath);
                foreach (string file in files)
                {
                    documents.Add(Path.GetFileName(file));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing documents: {ex.Message}");
        }
            
        return documents;
    }
    public void DeleteDocument(string fileName)
    {
        var filePath = Path.Combine(FolderPath, fileName);
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File '{fileName}' not found.");
            return;
        }

        try
        {
            File.Delete(filePath);
            Console.WriteLine($"File '{fileName}' deleted from local storage.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file '{fileName}': {ex.Message}");
        }
    }
}