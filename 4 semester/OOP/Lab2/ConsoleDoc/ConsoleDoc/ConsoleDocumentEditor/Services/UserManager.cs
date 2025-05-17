using System.Text.Json;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Models;
using ConsoleDoc.ConsoleDocumentEditor.Domain.Enums;
namespace ConsoleDoc.ConsoleDocumentEditor.Services;

public class UserManager
{
    private readonly string _dataFile = "users.json";
    private List<User> _users;

    public UserManager()
    {
        LoadUsers();
    }

    private void LoadUsers()
    {
        try
        {
            if (File.Exists(_dataFile))
            {
                var json = File.ReadAllText(_dataFile);
                _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            else
            {
                _users = new List<User>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading users: {ex.Message}");
            _users = new List<User>();
        }
    }
    private void SaveUsers()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_users, options);
            File.WriteAllText(_dataFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving users: {ex.Message}");
        }
    }
    public bool AddUser(User user)
    {
        if (_users.Any(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("User with this username already exists.");
            return false;
        }

        _users.Add(user);
        SaveUsers();
        return true;
    }
    public bool RemoveUser(string username)
    {
        var user = _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return false;
        }
        _users.Remove(user);
        SaveUsers();
        return true;
    }
    public List<User> ListUsers() => new List<User>(_users);
    public User? Login(string username, string password)
    {
        return _users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
            u.Password == password);
    }
}