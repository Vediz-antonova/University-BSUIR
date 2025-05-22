using ConsoleStudentRecordManagement.DTO;
namespace ConsoleStudentRecordManagement.Integration;

public interface IQuoteApiAdapter
{
    Task<QuoteDTO> GetMotivationalQuoteAsync();
}