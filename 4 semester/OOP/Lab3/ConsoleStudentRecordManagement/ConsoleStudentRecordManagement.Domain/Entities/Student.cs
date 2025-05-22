using System.Text.Json.Serialization;
namespace ConsoleStudentRecordManagement.Domain.Entities;

public class Student
{
    public string Name { get; private set; }
    public double Grade { get; private set; }

    [JsonConstructor]
    private Student(string name, double grade)
    {
        Name = name;
        Grade = grade;
    }

    public static Student Create(string name, double grade)
    {
        Domain.Validation.StudentValidator.Validate(name, grade);
        return new Student(name, grade);
    }
}