using ConsoleStudentRecordManagement.DataAccess;
using ConsoleStudentRecordManagement.Domain.Entities;
namespace ConsoleStudentRecordManagement.Tests
{
    public class FakeDataRepository : IDataRepository
    {
        public List<Student> Students { get; set; } = new List<Student>();

        public List<Student> GetAllStudents()
        {
            return Students;
        }

        public void SaveStudents(List<Student> students)
        {
            Students = students;
        }
    }
}