using ConsoleStudentRecordManagement.DTO;
using ConsoleStudentRecordManagement.Domain.Entities;
using ConsoleStudentRecordManagement.DataAccess;
namespace ConsoleStudentRecordManagement.Application.Commands;

public class EditStudentCommand : ICommand
{
    private readonly int _studentIndex;
    private readonly StudentDTO _studentDto;
    private readonly IDataRepository _repository;

    public EditStudentCommand(int studentIndex, StudentDTO dto, IDataRepository repository)
    {
        _studentIndex = studentIndex;
        _studentDto = dto;
        _repository = repository;
    }

    public Task ExecuteAsync()
    {
        var students = _repository.GetAllStudents();
        if (_studentIndex < 0 || _studentIndex >= students.Count)
            throw new IndexOutOfRangeException("Invalid student index.");

        students[_studentIndex] = Student.Create(_studentDto.Name, _studentDto.Grade);
        _repository.SaveStudents(students);
        return Task.CompletedTask;
    }
}