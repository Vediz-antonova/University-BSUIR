using ConsoleStudentRecordManagement.DTO;
using ConsoleStudentRecordManagement.Domain.Entities;
using ConsoleStudentRecordManagement.DataAccess;
using ConsoleStudentRecordManagement.Integration;
namespace ConsoleStudentRecordManagement.Application.Commands;

public class AddStudentCommand : ICommand
{
    private readonly StudentDTO _studentDto;
    private readonly IDataRepository _repository;
    private readonly IQuoteApiAdapter _quoteAdapter;

    public AddStudentCommand(StudentDTO dto, IDataRepository repository, IQuoteApiAdapter quoteAdapter)
    {
        _studentDto = dto;
        _repository = repository;
        _quoteAdapter = quoteAdapter;
    }

    public QuoteDTO Quote { get; private set; }

    public async Task ExecuteAsync()
    {
        var student = Student.Create(_studentDto.Name, _studentDto.Grade);
        var students = _repository.GetAllStudents();
        students.Add(student);
        _repository.SaveStudents(students);
        Quote = await _quoteAdapter.GetMotivationalQuoteAsync();
    }
}