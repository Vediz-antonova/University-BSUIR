using System.Text.Json;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Models;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Documents;
namespace ConsoleDoc.ConsoleDocumentEditor.Services;

public static class NotificationService
{
    private static readonly string _notificationsFile = "notifications.json";
    public static List<Notification> PendingNotifications { get; private set; } = LoadNotifications();
    
    private static List<Notification> LoadNotifications()
    {
        try
        {
            if (File.Exists(_notificationsFile))
            {
                var json = File.ReadAllText(_notificationsFile);
                return JsonSerializer.Deserialize<List<Notification>>(json) ?? new List<Notification>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading notifications: {ex.Message}");
        }

        return new List<Notification>();
    }
    private static void SaveNotifications()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(PendingNotifications, options);
            File.WriteAllText(_notificationsFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving notifications: {ex.Message}");
        }
    }
    public static void AddNotification(string message, string originator)
    {
        PendingNotifications.Add(new Notification(message, originator));
        SaveNotifications();
    }
    public static void ShowNotificationsForUser(User currentUser, Document doc)
    {
        var notificationsToShow = PendingNotifications
            .Where(n => !n.Originator.Equals(currentUser.Username, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (notificationsToShow.Any())
        {
            Console.WriteLine($"Notifications for document '{doc.Name}':");
            foreach (var notification in notificationsToShow)
            {
                Console.WriteLine(notification);
            }
            PendingNotifications.RemoveAll(n => !n.Originator.Equals(currentUser.Username, StringComparison.OrdinalIgnoreCase));
        }
    }
}