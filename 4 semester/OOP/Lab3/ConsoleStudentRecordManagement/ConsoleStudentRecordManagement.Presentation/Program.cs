using ConsoleStudentRecordManagement.DataAccess;
using ConsoleStudentRecordManagement.Integration;
namespace ConsoleStudentRecordManagement.Presentation;

public class Program
{
    public static async Task Main(string[] args)
    {
        IDataRepository repository = new JSONRepository();
        IQuoteApiAdapter quoteAdapter = new QuoteApiAdapter();
        ConsoleUI ui = new ConsoleUI(repository, quoteAdapter);
        await ui.RunAsync();
    }
}