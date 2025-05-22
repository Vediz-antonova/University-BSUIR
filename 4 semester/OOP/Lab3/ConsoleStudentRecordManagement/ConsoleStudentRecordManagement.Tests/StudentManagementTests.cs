using Xunit;
using ConsoleStudentRecordManagement.DTO;
using ConsoleStudentRecordManagement.Domain.Entities;
using ConsoleStudentRecordManagement.Application.Commands;
namespace ConsoleStudentRecordManagement.Tests
{
    public class StudentManagementTests
    {
        [Fact]
        public async Task AddStudentCommand_ValidStudent_AddsStudentAndReturnsQuote()
        {
            var fakeRepo = new FakeDataRepository();
            var fakeQuoteAdapter = new FakeQuoteApiAdapter();
            var studentDto = DTOFactory.CreateStudentDTO("JohnDoe", 5);
            var command = new AddStudentCommand(studentDto, fakeRepo, fakeQuoteAdapter);

            await command.ExecuteAsync();

            var students = fakeRepo.GetAllStudents();
            Assert.Single(students);
            Assert.Equal("JohnDoe", students[0].Name);
            Assert.Equal(5, students[0].Grade);
            Assert.NotNull(command.Quote);
            Assert.Equal("Test Quote", command.Quote.Content);
            Assert.Equal("Test Author", command.Quote.Author);
        }

        [Fact]
        public void CreateStudent_InvalidGrade_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => Student.Create("Alice", -10));
            Assert.Contains("Grade cannot be less than zero and more than 10.", exception.Message);
        }

        [Fact]
        public async Task QuoteApiAdapter_GetMotivationalQuoteAsync_ReturnsValidQuoteDTO()
        {
            var fakeQuoteAdapter = new FakeQuoteApiAdapter();
            var quote = await fakeQuoteAdapter.GetMotivationalQuoteAsync();

            Assert.NotNull(quote);
            Assert.Equal("Test Quote", quote.Content);
            Assert.Equal("Test Author", quote.Author);
        }
    }
}