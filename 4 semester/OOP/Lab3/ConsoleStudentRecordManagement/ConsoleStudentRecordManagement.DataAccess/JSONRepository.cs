using System.Text.Json;
using ConsoleStudentRecordManagement.Domain.Entities;
namespace ConsoleStudentRecordManagement.DataAccess;

public class JSONRepository : IDataRepository
{
    private readonly string _filePath = "students.json";

    public List<Student> GetAllStudents()
    {
        if (!File.Exists(_filePath))
            return new List<Student>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Student>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<Student>();
    }

    public void SaveStudents(List<Student> students)
    {
        var json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}