namespace ConsoleDoc.ConsoleDocumentEditor.Services;

public sealed class SettingsManager
{
    private static readonly object _lock = new();
    private static SettingsManager? _instance;
    
    public static SettingsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SettingsManager();
                    }
                }
            }

            return _instance;
        }
    }
    
    private SettingsManager() => ResetSettings();

    private ConsoleColor CurrentForegroundColor { get; set; }
    private ConsoleColor CurrentBackgroundColor { get; set; }
    public event Action? SettingsChanged;
    
    public void ResetSettings()
    {
        CurrentForegroundColor = ConsoleColor.Gray;
        CurrentBackgroundColor = ConsoleColor.Black;
        ApplySettings();
    }
    public void SetForegroundColor(ConsoleColor color)
    {
        CurrentForegroundColor = color;
        ApplySettings();
    }
    public void SetBackgroundColor(ConsoleColor color)
    {
        CurrentBackgroundColor = color;
        ApplySettings();
    }
    
    public void ApplySettings()
    {
        Console.ForegroundColor = CurrentForegroundColor;
        Console.BackgroundColor = CurrentBackgroundColor;
        SettingsChanged?.Invoke();
    }
}