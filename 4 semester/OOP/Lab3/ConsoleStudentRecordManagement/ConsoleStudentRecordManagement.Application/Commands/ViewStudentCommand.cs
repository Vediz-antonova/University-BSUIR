using ConsoleStudentRecordManagement.Domain.Entities;
using ConsoleStudentRecordManagement.DataAccess;
namespace ConsoleStudentRecordManagement.Application.Commands;

public class ViewStudentCommand : ICommand
{
    private readonly IDataRepository _repository;
    public List<Student> Students { get; private set; }

    public ViewStudentCommand(IDataRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync()
    {
        Students = _repository.GetAllStudents();
        return Task.CompletedTask;
    }
}