using System.Diagnostics;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Enums;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Commands;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Decorators;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Models;
using ConsoleDoc.ConsoleDocumentEditor.Services;
using ConsoleDoc.ConsoleDocumentEditor.Storage;
using ConsoleDoc.ConsoleDocumentEditor.Adapters;
namespace ConsoleDoc.ConsoleDocumentEditor.UI;

public static class Program
{
    private static DocumentManager? _documentManager;
    private static readonly Clipboard Clipboard = new Clipboard();
    private static readonly StorageContext StorageContext = new StorageContext();
    private static readonly UserManager _userManager = new UserManager();
    private static User? _currentUser;

    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Welcome to Console Document Editor!");
            LoginScreen();
            SelectStorageStrategy();
            DisplayMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            Console.WriteLine("Application will now exit.");
        }
    }
    
    private static void LoginScreen()
    {
        bool loggedIn = false;
        while (!loggedIn)
        {
            Console.WriteLine("\n--- User Login ---");
            Console.Write("Enter username: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? "";

            _currentUser = _userManager.Login(username, password);
            if (_currentUser != null)
            {
                Console.WriteLine($"Successfully logged in as {_currentUser.Username}.");

                var userSettings = UserSettingsManager.GetSettingsForUser(_currentUser.Username);
                try
                {
                    var fgColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), userSettings.ForegroundColor, true);
                    SettingsManager.Instance.SetForegroundColor(fgColor);
                }
                catch { }
                try
                {
                    var bgColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), userSettings.BackgroundColor, true);
                    SettingsManager.Instance.SetBackgroundColor(bgColor);
                }
                catch { }
                loggedIn = true;
            }
            else
            {
                Console.WriteLine("Login failed. User not found or incorrect password.");
                Console.Write("Do you want to register a new account? (Y/N): ");
                string response = Console.ReadLine() ?? "";
                if (response.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    RegisterUser();
                else
                    Console.WriteLine("Please try logging in again.");
            }
        }
    }
    private static void RegisterUser()
    {
        Console.WriteLine("\n--- User Registration ---");
        Console.Write("Enter desired username: ");
        string username = Console.ReadLine() ?? "";
        Console.Write("Enter desired password: ");
        string password = Console.ReadLine() ?? "";
        
        var newUser = new User(username, password);
        if (_userManager.AddUser(newUser))
        {
            Console.WriteLine("Registration successful. You can now log in.");
        }
        else
        {
            Console.WriteLine("Registration failed. A user with that username may already exist.");
        }
    }

    private static void SelectStorageStrategy()
    {
        bool strategySet = false;
        while (!strategySet)
        {
            Console.WriteLine("\n--- Select Storage Strategy ---");
            Console.WriteLine("1. Local Storage");
            Console.WriteLine("2. Cloud Storage");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
                    var docsFolder = Path.Combine(projectDirectory ?? string.Empty, "..\\..\\Docs\\");
                    var localStore = new LocalStorage(docsFolder);
                    StorageContext.SetStorageStrategy(localStore);
                    _documentManager = new DocumentManager(new AdapterFactory(), localStore);
                    Console.WriteLine("Local storage selected.");
                    strategySet = true;
                    break;
                case "2":
                    var cloudStore = new CloudStorage();
                    StorageContext.SetStorageStrategy(cloudStore);
                    _documentManager = new DocumentManager(new AdapterFactory(), cloudStore);
                    Console.WriteLine("Cloud storage selected.");
                    strategySet = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void DisplayMenu()
    {
        if (_currentUser == null)
        {
            Console.WriteLine("Error: No user logged in!");
            return;
        }

        while (true)
        {
            Console.WriteLine($"\n--- Main Menu (User: {_currentUser.Username}) ---");
            Console.WriteLine("1. Create Document");
            Console.WriteLine("2. List Documents");
            Console.WriteLine("3. View Document Content");
            Console.WriteLine("4. Edit Document (only Admin or Editor)");
            Console.WriteLine("5. Save Document (only Admin or Editor)");
            Console.WriteLine("6. Load Document");
            Console.WriteLine("7. Undo Last Action");
            Console.WriteLine("8. Redo Last Action");
            Console.WriteLine("9. Delete Document (only Admin)");
            //Console.WriteLine("10. Save Document Version");
            Console.WriteLine("11. View Document Versions");
            Console.WriteLine("12. Restore Document Version (only Admin or Editor)");
            Console.WriteLine("13. Console Settings");
            Console.WriteLine("14. Manage Users");
            Console.WriteLine("0. Exit");

            Console.Write("Select an option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateDocument();
                    break;
                case "2":
                    ListDocuments();
                    break;
                case "3":
                {
                    Document? doc = ChooseDocument();
                    if (doc != null)
                        ViewDocument(doc);
                }
                    break;
                case "4":
                {
                    Document? doc = ChooseDocument();
                    if (doc != null)
                        EditDocument(doc);
                }
                    break;
                case "5":
                {
                    Document? doc = ChooseDocument();
                    if (doc != null)
                        SaveDocument(doc);
                }
                    break;
                case "6":
                    LoadDocument();
                    break;
                case "7":
                    Undo();
                    break;
                case "8":
                    Redo();
                    break;
                case "9":
                {
                    Document? doc = ChooseDocument();
                    if (doc != null)
                        DeleteDocument(doc);
                }
                    break;
                /*case "10":
                {
                    Document? doc = ChooseDocument();
                    if (doc != null)
                        SaveDocumentVersion(doc);
                }
                    break;*/
                case "11":
                {
                    Document? doc = ChooseDocument();
                    if (doc != null)
                        ViewDocumentVersions(doc);
                }
                    break;
                case "12":
                {
                    Document? doc = ChooseDocument();
                    if (doc != null)
                    {
                        var versions = StorageContext.GetDocumentVersions(doc.Name, "txt");
                        if (versions == null || versions.Count == 0)
                        {
                            Console.WriteLine($"No versions available for document '{doc.Name}'.");
                            break;
                        }

                        Console.WriteLine("Available versions:");
                        for (int i = 0; i < versions.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {versions[i]}");
                        }

                        Console.Write("Select a version (by number): ");
                        if (!int.TryParse(Console.ReadLine(), out int versionIndex) || versionIndex < 1 ||
                            versionIndex > versions.Count)
                        {
                            Console.WriteLine("Invalid version number.");
                            break;
                        }

                        RestoreDocumentVersion(doc, versionIndex);
                    }
                }
                    break;
                case "13":
                    ConfigureConsoleSettings();
                    break;
                case "14":
                    DisplayUserManagementMenu(ChooseDocument());
                    break;
                case "0":
                    Console.WriteLine("Exiting the application. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static string? ReadInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    private static int ReadInt(string message)
    {
        while (true)
        {
            string? input = ReadInput(message);
            if (int.TryParse(input, out int result))
            {
                return result;
            }

            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    private static Document? ChooseDocument()
    {
        try
        {
            ListDocuments();
            int index = ReadInt("Select a document (by number): ");
            var docs = _documentManager?.ListDocuments();
            if (docs == null || index < 1 || index > docs.Count)
            {
                Console.WriteLine("Invalid document number.");
                return null;
            }

            return docs[index - 1];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error choosing document: {ex.Message}");
            return null;
        }
    }

    private static void CreateDocument()
    {
        try
        {
            Console.WriteLine("\n--- Create Document ---");
            string name = ReadInput("Enter document title: ") ?? string.Empty;
            Console.WriteLine("Select document type:");
            Console.WriteLine("1. Plain Text");
            Console.WriteLine("2. Markdown");
            Console.WriteLine("3. Rich Text");
            Console.WriteLine("4. Json");
            Console.WriteLine("5. Xml");
            string? typeInput = ReadInput("Choose an option: ");

            DocumentType type;
            switch (typeInput)
            {
                case "1":
                    type = DocumentType.PlainText;
                    break;
                case "2":
                    type = DocumentType.Markdown;
                    break;
                case "3":
                    type = DocumentType.RichText;
                    break;
                case "4":
                    type = DocumentType.Json;
                    break;
                case "5":
                    type = DocumentType.Xml;
                    break;
                default:
                    Console.WriteLine("Invalid document type selected.");
                    return;
            }

            var document = _documentManager?.CreateDocument(type, name, _currentUser.ToString());
            if (document == null)
            {
                Console.WriteLine("Failed to create document.");
            }
            else
            {
                Console.WriteLine($"Document '{document.Name}' of type '{type}' created successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating document: {ex.Message}");
        }
    }

    private static void ListDocuments()
    {
        try
        {
            Console.WriteLine("\n--- List of Documents ---");
            var documents = _documentManager?.ListDocuments();
            if (documents == null || documents.Count == 0)
            {
                Console.WriteLine("No documents available.");
                return;
            }

            for (int i = 0; i < documents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {documents[i]?.Name} ({documents[i]?.GetType().Name})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing documents: {ex.Message}");
        }
    }

    private static void SaveDocument(Document? document)
    {
        try
        {
            if (document == null)
            {
                Console.WriteLine("No document selected to save.");
                return;
            }

            if (_documentManager.GetUserRole(document, _currentUser.ToString()) != UserRole.Viewer)
            {
                _documentManager?.SaveDocument(document);
                SaveDocumentVersion(document);
                Console.WriteLine($"Document '{document.Name}' saved as '{document.Name}'.");
            }
            else
            {
                Console.WriteLine($"User {_currentUser.ToString()} does not have sufficient rights.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving document: {ex.Message}");
        }
    }
    private static void LoadDocument()
    {
        try
        {
            var availableFiles = StorageContext.ListDocuments();
            if (availableFiles.Count == 0)
            {
                Console.WriteLine("No files available in storage.");
                return;
            }

            Console.WriteLine("\nAvailable files in storage:");
            for (int i = 0; i < availableFiles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableFiles[i]}");
            }

            Console.Write("\nEnter file name to load (with extension): ");
            var fileName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileName) || !fileName.Contains('.'))
            {
                Console.WriteLine("Invalid file name provided.");
                return;
            }

            var format = fileName.Substring(fileName.LastIndexOf('.') + 1);
            fileName = fileName.Substring(0, fileName.LastIndexOf('.'));

            DocumentType type;
            switch (format.ToLower())
            {
                case "txt":
                    type = DocumentType.PlainText;
                    break;
                case "md":
                    type = DocumentType.Markdown;
                    break;
                case "rtf":
                    type = DocumentType.RichText;
                    break;
                case "json":
                    type = DocumentType.Json;
                    break;
                case "xml":
                    type = DocumentType.Xml;
                    break;
                default:
                    Console.WriteLine("Unsupported file type.");
                    return;
            }

            if (_documentManager.ListDocuments().Any(d => d.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("This file is already loaded.");
                return;
            }

            var document = _documentManager.LoadDocument(fileName, format, type, _currentUser.ToString());
            Console.WriteLine($"Document '{document.Name}' loaded successfully with content:");
            Console.WriteLine(document.Content);

            NotificationService.ShowNotificationsForUser(_currentUser, document);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading document: {ex.Message}");
        }
    }

    private static void ViewDocument(Document? document)
    {
        try
        {
            if (document == null)
            {
                Console.WriteLine("No document selected to view.");
                return;
            }

            Console.WriteLine($"\n--- Viewing Document: {document.Name} ---");
            Console.WriteLine($"Content: \n{document.Content}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error viewing document: {ex.Message}");
        }
    }

    private static void DeleteDocument(Document? document)
    {
        try
        {
            if (document == null)
            {
                Console.WriteLine("No document selected to delete.");
                return;
            }

            if (_documentManager.GetUserRole(document, _currentUser.ToString()) == UserRole.Administrator)
            {
                _documentManager?.DeleteDocument(document);
                Console.WriteLine($"Document '{document.Name}' deleted.");
            } 
            else
            {
                Console.WriteLine($"User {_currentUser.ToString()} does not have sufficient rights.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting document: {ex.Message}");
        }
    }

    private static void Undo()
    {
        try
        {
            _documentManager?.Undo();
            Console.WriteLine("Undo operation executed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error performing undo: {ex.Message}");
        }
    }

    private static void Redo()
    {
        try
        {
            _documentManager?.Redo();
            Console.WriteLine("Redo operation executed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error performing redo: {ex.Message}");
        }
    }

    private static void SaveDocumentVersion(Document? document)
    {
        try
        {
            if (document == null)
            {
                Console.WriteLine("No document selected to save version.");
                return;
            }

            string format = document.Type switch
            {
                DocumentType.PlainText => "txt",
                DocumentType.Markdown  => "md",
                DocumentType.RichText  => "rtf",
                DocumentType.Json      => "json",
                DocumentType.Xml       => "xml",
                _                      => "txt"
            };

            var versions = StorageContext.GetDocumentVersions(document.Name, format);

            int versionNumber = versions.Count + 1;

            StorageContext.SaveVersion(document.Name, document.Content, format, versionNumber);

            Console.WriteLine($"Document version {versionNumber} for '{document.Name}' saved.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving document version: {ex.Message}");
        }
    }
    private static void ViewDocumentVersions(Document? document)
    {
        try
        {
            if (document == null)
            {
                Console.WriteLine("No document selected to view versions.");
                return;
            }

            string format = document.Type switch
            {
                DocumentType.PlainText => "txt",
                DocumentType.Markdown  => "md",
                DocumentType.RichText  => "rtf",
                DocumentType.Json      => "json",
                DocumentType.Xml       => "xml",
                _ => "txt"
            };

            var versions = StorageContext.GetDocumentVersions(document.Name, format);
            if (versions.Count == 0)
            {
                Console.WriteLine("No versions available.");
                return;
            }

            Console.WriteLine("Document Versions:");
            for (int i = 0; i < versions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {versions[i]}");
            }

            Console.Write("Enter version number to view content (or 0 to exit): ");
            if (!int.TryParse(Console.ReadLine(), out int versionIndex) || versionIndex < 0 ||
                versionIndex > versions.Count)
            {
                Console.WriteLine("Invalid version number.");
                return;
            }

            if (versionIndex == 0)
            {
                return;
            }

            string? versionContent = StorageContext.RestoreVersion(document.Name, versionIndex, format);
            if (versionContent != null)
            {
                Console.WriteLine($"\n--- Content of version {versionIndex} of document '{document.Name}' ---");
                Console.WriteLine(versionContent);
                Console.WriteLine("-----------------------------------------------\n");
            }
            else
            {
                Console.WriteLine($"Failed to load content for version {versionIndex} of document '{document.Name}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error viewing document versions: {ex.Message}");
        }
    }
    private static void RestoreDocumentVersion(Document? document, int versionNumber)
    {
        try
        {
            if (document == null)
            {
                Console.WriteLine("No document selected to restore version.");
                return;
            }

            if (_documentManager.GetUserRole(document, _currentUser.ToString()) != UserRole.Viewer)
            {
                var restoredContent = StorageContext!.RestoreVersion(document.Name, versionNumber, "txt");
                if (restoredContent != null)
                {
                    document.Content = restoredContent;
                    Console.WriteLine($"Document '{document.Name}' restored to version {versionNumber}.");
                }
                else
                {
                    Console.WriteLine($"Failed to restore version {versionNumber} of document '{document.Name}'.");
                }
            }
            else
            {
                Console.WriteLine($"User {_currentUser.ToString()} does not have sufficient rights.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error restoring document version: {ex.Message}");
        }
    }

    private static void ConfigureConsoleSettings()
    {
        bool exitSettings = false;
        while (!exitSettings)
        {
            Console.WriteLine("\n--- Console Settings Menu ---");
            Console.WriteLine("1. Reset settings to default");
            Console.WriteLine("2. Set foreground color");
            Console.WriteLine("3. Set background color");
            Console.WriteLine("0. Return to Main Menu");
            Console.Write("Select an option: ");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    SettingsManager.Instance.ResetSettings();
                    if (_currentUser != null)
                    {
                        var userSettings = UserSettingsManager.GetSettingsForUser(_currentUser.Username);
                        userSettings.ForegroundColor = "Gray";
                        userSettings.BackgroundColor = "Black";
                        UserSettingsManager.UpdateSettings(userSettings);
                    }

                    Console.WriteLine("Console settings reset to default.");
                    break;
                case "2":
                    DisplayAvailableColors();
                    Console.Write("Enter foreground color: ");
                    string fg = Console.ReadLine() ?? "Gray";
                    try
                    {
                        var fgColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), fg, true);
                        SettingsManager.Instance.SetForegroundColor(fgColor);
                        if (_currentUser != null)
                        {
                            var userSettings = UserSettingsManager.GetSettingsForUser(_currentUser.Username);
                            userSettings.ForegroundColor = fgColor.ToString();
                            UserSettingsManager.UpdateSettings(userSettings);
                        }

                        Console.WriteLine($"Foreground color set to {fgColor}.");
                    }
                    catch
                    {
                        Console.WriteLine("Invalid color input.");
                    }

                    break;
                case "3":
                    DisplayAvailableColors();
                    Console.Write("Enter background color: ");
                    string bg = Console.ReadLine() ?? "Black";
                    try
                    {
                        var bgColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), bg, true);
                        SettingsManager.Instance.SetBackgroundColor(bgColor);
                        if (_currentUser != null)
                        {
                            var userSettings = UserSettingsManager.GetSettingsForUser(_currentUser.Username);
                            userSettings.BackgroundColor = bgColor.ToString();
                            UserSettingsManager.UpdateSettings(userSettings);
                        }

                        Console.WriteLine($"Background color set to {bgColor}.");
                    }
                    catch
                    {
                        Console.WriteLine("Invalid color input.");
                    }

                    break;
                case "0":
                    exitSettings = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
    private static void DisplayAvailableColors()
    {
        Console.WriteLine("Available colors:");
        int i = 0;
        foreach (var colorName in Enum.GetNames(typeof(ConsoleColor)))
        {
            Console.WriteLine(i + ". "+colorName);
            i++;
        }
    }

    private static void DisplayUserManagementMenu(Document? document)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n--- User Management Menu ---");
            Console.WriteLine("1. Add User (only Admin)");
            Console.WriteLine("2. List Users");
            Console.WriteLine("3. List Users For This Document");
            Console.WriteLine("4. Change User Role (only Admin)");
            Console.WriteLine("5. Remove User (only Admin)");
            Console.WriteLine("6. Change User");
            Console.WriteLine("0. Return to Main Menu");
            Console.Write("Select an option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddUser(document);
                    break;
                case "2":
                    ListUsers();
                    break;
                case "3":
                    ListUsersForDocument(document);
                    break;
                case "4":
                    ChangeUserRole(document);
                    break;
                case "5":
                    RemoveUser(document);
                    break;
                case "6":
                    LoginScreen();
                    NotificationService.ShowNotificationsForUser(_currentUser, document);
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
    private static void AddUser(Document document)
    {
        if (document == null)
        {
            Console.WriteLine("No document selected to restore version.");
            return;
        }
        
        if (_documentManager.GetUserRole(document, _currentUser.ToString()) == UserRole.Administrator)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username cannot be empty.");
                return;
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? "";
            Console.WriteLine("Select user role:");
            Console.WriteLine("1. Viewer");
            Console.WriteLine("2. Editor");
            Console.WriteLine("3. Administrator");
            Console.Write("Enter your choice: ");
            string? roleInput = Console.ReadLine();
            UserRole role;
            switch (roleInput)
            {
                case "1":
                    role = UserRole.Viewer;
                    break;
                case "2":
                    role = UserRole.Editor;
                    break;
                case "3":
                    role = UserRole.Administrator;
                    break;
                default:
                    Console.WriteLine("Invalid role selected.");
                    return;
            }

            var user = new User(username, password);
            _userManager.AddUser(user);
            _documentManager.ChangeDocumentRole(document, user.ToString(), role, _currentUser.ToString());
            _documentManager.SaveDocument(document);
        }
        else
        {
            Console.WriteLine($"User {_currentUser.ToString()} does not have sufficient rights.");
        }
    }
    private static void ListUsers()
    {
        var users = _userManager.ListUsers();
        if (users.Count == 0)
        {
            Console.WriteLine("No users found.");
            return;
        }
        Console.WriteLine("\n--- List of Users ---");
        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {users[i]}");
        }
    }
    private static void ListUsersForDocument(Document document) {
        if (document == null)
        {
            Console.WriteLine("No document selected to restore version.");
            return;
        }

        var users = _documentManager.ListUsersForDocument(document);
        if (users.Count == 0) {
            Console.WriteLine("No users found.");
            return;
        }

        Console.WriteLine("--- List of Users ---");
        int index = 1;
        foreach (var user in users) {
            Console.WriteLine($"{index++}. {user.Key}: {user.Value}");
        }
    }
    private static void ChangeUserRole(Document document)
    {
        if (document == null)
        {
            Console.WriteLine("No document selected to restore version.");
            return;
        }
        
        if (_documentManager.GetUserRole(document, _currentUser.ToString()) == UserRole.Administrator)
        {
            Console.Write("Enter username to change role: ");
            string username = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Select new role:");
            Console.WriteLine("1. Viewer");
            Console.WriteLine("2. Editor");
            Console.WriteLine("3. Administrator");
            Console.Write("Enter your choice: ");
            string? roleInput = Console.ReadLine();
            UserRole newRole;
            switch (roleInput)
            {
                case "1":
                    newRole = UserRole.Viewer;
                    break;
                case "2":
                    newRole = UserRole.Editor;
                    break;
                case "3":
                    newRole = UserRole.Administrator;
                    break;
                default:
                    Console.WriteLine("Invalid role selected.");
                    return;
            }

            _documentManager.ChangeDocumentRole(document, username, newRole, _currentUser.ToString());
            _documentManager.SaveDocument(document);
        }
        else
        {
            Console.WriteLine($"User {_currentUser.ToString()} does not have sufficient rights.");
        }
    }
    private static void RemoveUser(Document document)
    {
        if (document == null)
        {
            Console.WriteLine("No document selected to restore version.");
            return;
        }
        
        if (_documentManager.GetUserRole(document, _currentUser.ToString()) == UserRole.Administrator)
        {
            Console.Write("Enter username to remove: ");
            string username = Console.ReadLine() ?? string.Empty;
            _userManager.RemoveUser(username);
        }
        else
        {
            Console.WriteLine($"User {_currentUser.ToString()} does not have sufficient rights.");
        }
    }

    private static void EditDocument(Document? document)
    {
        if (document == null)
        {
            Console.WriteLine("No document selected for editing.");
            return;
        }

        if (_documentManager.GetUserRole(document, _currentUser.ToString()) != UserRole.Viewer)
        {
            Console.WriteLine($"Modifying document '{document.Name}'.");
            Console.WriteLine($"Current content: {document.Content}");

            Console.WriteLine("Choose operation:");
            Console.WriteLine("[1] Edit Entire Text");
            Console.WriteLine("[2] Insert Text");
            Console.WriteLine("[3] Delete Text");
            Console.WriteLine("[4] Copy Text");
            Console.WriteLine("[5] Cut Text");
            Console.WriteLine("[6] Paste Text");
            Console.WriteLine("[7] Find Text");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    HandleEditEntireText(document);
                    break;
                case "2":
                    HandleInsertText(document);
                    break;
                case "3":
                    HandleDeleteText(document);
                    break;
                case "4":
                    HandleCopyText(document);
                    break;
                case "5":
                    HandleCutText(document);
                    break;
                case "6":
                    HandlePasteText(document);
                    break;
                case "7":
                    HandleFindText(document);
                    break;
                default:
                    Console.WriteLine("Invalid operation selected.");
                    break;
            }
        }
        else
        {
            Console.WriteLine($"User {_currentUser.ToString()} does not have sufficient rights.");
        }
    }

    #region Edit Command Handlers
    private static void HandleEditEntireText(Document document)
    {
        string newContent, styleChoice;
        var newDoc = new PlainTextDocument("newDoc");

        Console.Write("Enter new content for the document: ");
        newContent = Console.ReadLine() ?? string.Empty;
        newDoc.Content = newContent;

        string formattedContent;

        if (document.Type == DocumentType.Markdown)
        {
            Console.WriteLine("Choose text style:");
            Console.WriteLine("[1] Bold");
            Console.WriteLine("[2] Italic");
            Console.WriteLine("[3] Heading 1");
            Console.WriteLine("[4] Heading 2");
            Console.WriteLine("[5] Heading 3");
            Console.WriteLine("[6] None");
            Console.Write("Enter your choice: ");
            styleChoice = Console.ReadLine() ?? "6";

            formattedContent = styleChoice switch
            {
                "1" => new BoldDecorator(newDoc).Content,
                "2" => new ItalicDecorator(newDoc).Content,
                "3" => new Heading1Decorator(newDoc).Content,
                "4" => new Heading2Decorator(newDoc).Content,
                "5" => new Heading3Decorator(newDoc).Content,
                _ => newContent
            };
        }
        else
        {
            formattedContent = newContent;
        }

        var editCommand = new Domain.Commands.EditTextCommand(document, formattedContent);
        ExecuteCommandAndNotify(editCommand, document);

        Console.WriteLine($"Formatted content: {document?.Content}");
    }
    private static void HandleInsertText(Document document)
    {
        string newContent, styleChoice;
        var newDoc = new PlainTextDocument("newDoc");

        Console.Write("Enter text to insert: ");
        newContent = Console.ReadLine() ?? string.Empty;
        string textToInsert;

        if (document.Type == DocumentType.Markdown)
        {
            Console.WriteLine("Choose text style:");
            Console.WriteLine("[1] Bold");
            Console.WriteLine("[2] Italic");
            Console.WriteLine("[3] Heading 1");
            Console.WriteLine("[4] Heading 2");
            Console.WriteLine("[5] Heading 3");
            Console.WriteLine("[6] None");
            Console.Write("Enter your choice: ");
            styleChoice = Console.ReadLine() ?? "6";
            newDoc.Content = newContent;

            textToInsert = styleChoice switch
            {
                "1" => new BoldDecorator(newDoc).Content,
                "2" => new ItalicDecorator(newDoc).Content,
                "3" => new Heading1Decorator(newDoc).Content,
                "4" => new Heading2Decorator(newDoc).Content,
                "5" => new Heading3Decorator(newDoc).Content,
                _ => newContent
            };
        }
        else
        {
            newDoc.Content = newContent;
            textToInsert = newContent;
        }

        Console.Write("Enter position to insert at: ");
        if (int.TryParse(Console.ReadLine(), out var positionInsert))
        {
            var insertCommand = new Domain.Commands.InsertTextCommand(document, textToInsert, positionInsert);
            ExecuteCommandAndNotify(insertCommand, document);

            Console.WriteLine($"Text inserted and formatted: {document?.Content}");
        }
        else
        {
            Console.WriteLine("Invalid position.");
        }
    }
    private static void HandleDeleteText(Document document)
    {
        Console.Write("Enter start position to delete: ");
        if (int.TryParse(Console.ReadLine(), out var startPosition))
        {
            Console.Write("Enter length to delete: ");
            if (int.TryParse(Console.ReadLine(), out var length))
            {
                var deleteCommand = new Domain.Commands.DeleteTextCommand(document, startPosition, length);
                ExecuteCommandAndNotify(deleteCommand, document);
                Console.WriteLine($"Text deleted: {document.Content}");
            }
            else
            {
                Console.WriteLine("Invalid length.");
            }
        }
        else
        {
            Console.WriteLine("Invalid start position.");
        }
    }
    private static void HandleCopyText(Document document)
    {
        Console.Write("Enter start position to copy: ");
        if (int.TryParse(Console.ReadLine(), out var copyStart))
        {
            Console.Write("Enter length to copy: ");
            if (int.TryParse(Console.ReadLine(), out var copyLength))
            {
                var copyCommand = new Domain.Commands.CopyTextCommand(document, copyStart, copyLength, Clipboard);
                _documentManager?.ExecuteCommand(copyCommand);
            }
            else
            {
                Console.WriteLine("Invalid length.");
            }
        }
        else
        {
            Console.WriteLine("Invalid start position.");
        }
    }
    private static void HandleCutText(Document document)
    {
        Console.Write("Enter start position to cut: ");
        if (int.TryParse(Console.ReadLine(), out var cutStart))
        {
            Console.Write("Enter length to cut: ");
            if (int.TryParse(Console.ReadLine(), out var cutLength))
            {
                var cutCommand = new Domain.Commands.CutTextCommand(document, cutStart, cutLength, Clipboard);
                ExecuteCommandAndNotify(cutCommand, document);
            }
            else
            {
                Console.WriteLine("Invalid length.");
            }
        }
        else
        {
            Console.WriteLine("Invalid start position.");
        }
    }
    private static void HandlePasteText(Document document)
    {
        Console.Write("Enter position to paste text: ");
        if (int.TryParse(Console.ReadLine(), out var pastePosition))
        {
            var pasteCommand = new Domain.Commands.PasteTextCommand(document, Clipboard, pastePosition);
            ExecuteCommandAndNotify(pasteCommand, document);
            Console.WriteLine($"Content after paste: {document.Content}");
        }
        else
        {
            Console.WriteLine("Invalid position.");
        }
    }
    private static void HandleFindText(Document document)
    {
        Console.Write("Enter text to find: ");
        var searchText = Console.ReadLine();
        var findCommand = new Domain.Commands.FindTextCommand(document, searchText);
        _documentManager?.ExecuteCommand(findCommand);
    }

    #endregion

    private static string ApplyTextStyle(string content)
    {
        Console.WriteLine("Choose text style:");
        Console.WriteLine("[1] Bold");
        Console.WriteLine("[2] Italic");
        Console.WriteLine("[3] Heading 1");
        Console.WriteLine("[4] Heading 2");
        Console.WriteLine("[5] Heading 3");
        Console.WriteLine("[6] None");
        Console.Write("Enter your choice: ");
        string styleChoice = Console.ReadLine() ?? "6";

        var tempDoc = new PlainTextDocument("tempDoc") { Content = content };

        return styleChoice switch
        {
            "1" => new BoldDecorator(tempDoc).Content,
            "2" => new ItalicDecorator(tempDoc).Content,
            "3" => new Heading1Decorator(tempDoc).Content,
            "4" => new Heading2Decorator(tempDoc).Content,
            "5" => new Heading3Decorator(tempDoc).Content,
            _ => content,
        };
    }
    
    private static void ExecuteCommandAndNotify(IDocumentCommand command, Document document)
    {
        _documentManager?.ExecuteCommand(command);
        string message = $"Документ '{document.Name}' был изменён.";
        string originator = _currentUser?.Username ?? "Unknown";
        NotificationService.AddNotification(message, originator);
    }
}