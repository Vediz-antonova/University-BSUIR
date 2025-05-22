using ConsoleStudentRecordManagement.Domain.Entities;
namespace ConsoleStudentRecordManagement.DataAccess;

public interface IDataRepository
{
    List<Student> GetAllStudents();
    void SaveStudents(List<Student> students);
}