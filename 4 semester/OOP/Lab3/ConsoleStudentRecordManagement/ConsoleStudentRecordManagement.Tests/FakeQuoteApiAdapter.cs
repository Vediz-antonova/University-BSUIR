using ConsoleStudentRecordManagement.DTO;
using ConsoleStudentRecordManagement.Integration;
namespace ConsoleStudentRecordManagement.Tests
{
    public class FakeQuoteApiAdapter : IQuoteApiAdapter
    {
        public Task<QuoteDTO> GetMotivationalQuoteAsync()
        {
            return Task.FromResult(DTOFactory.CreateQuoteDTO("Test Quote", "Test Author"));
        }
    }
}