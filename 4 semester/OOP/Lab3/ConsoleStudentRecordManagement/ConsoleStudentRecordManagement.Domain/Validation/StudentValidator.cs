using System.Text.RegularExpressions;
namespace ConsoleStudentRecordManagement.Domain.Validation;

public static class StudentValidator
{
    public static void Validate(string name, double grade)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");
        if (!Regex.IsMatch(name, @"^[A-Za-z]+[0-9]*$"))
            throw new ArgumentException("Name must consist of letters with optional digits at the end.");
        if (grade < 0 || grade > 10)
            throw new ArgumentException("Grade cannot be less than zero and more than 10.");
    }
}