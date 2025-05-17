using System.Text.Json;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Models;
namespace ConsoleDoc.ConsoleDocumentEditor.Services;

public class UserSettingsManager
{
    private static readonly string settingsFile = "userSettings.json";
    private static Dictionary<string, UserSettings> _userSettings = LoadSettings();

    private static Dictionary<string, UserSettings> LoadSettings()
    {
        try
        {
            if (File.Exists(settingsFile))
            {
                string json = File.ReadAllText(settingsFile);
                return JsonSerializer.Deserialize<Dictionary<string, UserSettings>>(json) ??
                       new Dictionary<string, UserSettings>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading user settings: {ex.Message}");
        }

        return new Dictionary<string, UserSettings>();
    }

    private static void SaveSettings()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_userSettings, options);
            File.WriteAllText(settingsFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving user settings: {ex.Message}");
        }
    }

    public static UserSettings GetSettingsForUser(string username)
    {
        if (_userSettings.ContainsKey(username))
            return _userSettings[username];

        var defaultSettings = new UserSettings
        {
            Username = username,
            ForegroundColor = "Gray",
            BackgroundColor = "Black",
        };
        _userSettings[username] = defaultSettings;
        SaveSettings();
        return defaultSettings;
    }

    public static void UpdateSettings(UserSettings settings)
    {
        _userSettings[settings.Username] = settings;
        SaveSettings();
    }
}