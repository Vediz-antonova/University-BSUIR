namespace ConsoleDoc.ConsoleDocumentEditor.Storage;

public class StorageContext
    {
        private IStorage _strategy;

        public void SetStorageStrategy(IStorage strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
        
        public void SaveVersion(string documentName, string content, string format, int versionNumber)
        {
            if (_strategy is LocalStorage localStorage)
            {
                var versionsFolder = Path.Combine(localStorage.FolderPath, "Versions");
                try
                {
                    if (!Directory.Exists(versionsFolder))
                    {
                        Directory.CreateDirectory(versionsFolder);
                    }

                    var versionFileName = Path.Combine(versionsFolder, $"{documentName}_v{versionNumber}.{format}");
                    File.WriteAllText(versionFileName, content);
                    Console.WriteLine($"Version {versionNumber} of document '{documentName}' saved as '{versionFileName}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving version of document '{documentName}': {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Version control is only supported for local storage.");
            }
        }

        public List<string> GetDocumentVersions(string documentName, string format)
        {
            if (_strategy is LocalStorage localStorage)
            {
                var versionsFolder = Path.Combine(localStorage.FolderPath, "Versions");
                if (!Directory.Exists(versionsFolder))
                {
                    return new List<string>();
                }

                try
                {
                    var versionFiles = Directory.GetFiles(versionsFolder, $"{documentName}_v*.{format}");
                    return versionFiles.Select(Path.GetFileName).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving versions of document '{documentName}': {ex.Message}");
                    return new List<string>();
                }
            }

            Console.WriteLine("Version control is only supported for local storage.");
            return new List<string>();
        }

        public string? RestoreVersion(string documentName, int versionNumber, string format)
        {
            if (_strategy is LocalStorage localStorage)
            {
                var versionsFolder = Path.Combine(localStorage.FolderPath, "Versions");
                var versionFileName = Path.Combine(versionsFolder, $"{documentName}_v{versionNumber}.{format}");
                try
                {
                    if (!File.Exists(versionFileName))
                    {
                        Console.WriteLine($"Version {versionNumber} of document '{documentName}' does not exist.");
                        return null;
                    }
                    return File.ReadAllText(versionFileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error restoring version {versionNumber} of document '{documentName}': {ex.Message}");
                    return null;
                }
            }

            Console.WriteLine("Version control is only supported for local storage.");
            return null;
        }

        public List<string> ListDocuments()
        {
            if (_strategy == null)
            {
                throw new InvalidOperationException("Storage strategy is not set.");
            }

            try
            {
                return _strategy.ListDocuments();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing documents: {ex.Message}");
                return new List<string>();
            }
        }
    }