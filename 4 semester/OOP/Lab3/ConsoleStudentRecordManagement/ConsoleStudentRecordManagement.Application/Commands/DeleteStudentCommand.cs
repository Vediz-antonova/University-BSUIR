using ConsoleStudentRecordManagement.DataAccess;
namespace ConsoleStudentRecordManagement.Application.Commands;

public class DeleteStudentCommand : ICommand
{
    private readonly int _studentIndex;
    private readonly IDataRepository _repository;

    public DeleteStudentCommand(int studentIndex, IDataRepository repository)
    {
        _studentIndex = studentIndex;
        _repository = repository;
    }

    public Task ExecuteAsync()
    {
        var students = _repository.GetAllStudents();
        if (_studentIndex < 0 || _studentIndex >= students.Count)
            throw new IndexOutOfRangeException("Invalid student index.");

        students.RemoveAt(_studentIndex);
        _repository.SaveStudents(students);
        return Task.CompletedTask;
    }
}