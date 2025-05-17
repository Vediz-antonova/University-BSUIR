using ConsoleDoc.ConsoleDocumentEditor.Domain.Enums;
namespace ConsoleDoc.ConsoleDocumentEditor.Domain.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; } 

    public User(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.", nameof(username));
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        Username = username;
        Password = password;
    }

    public override string ToString() {
        return Username;
    }
}