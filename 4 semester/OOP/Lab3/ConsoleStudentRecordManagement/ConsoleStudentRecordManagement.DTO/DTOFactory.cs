namespace ConsoleStudentRecordManagement.DTO;

public static class DTOFactory
{
    public static StudentDTO CreateStudentDTO(string name, double grade)
    {
        return new StudentDTO { Name = name, Grade = grade };
    }
    
    public static QuoteDTO CreateQuoteDTO(string content, string author)
    {
        return new QuoteDTO { Content = content, Author = author };
    }
}