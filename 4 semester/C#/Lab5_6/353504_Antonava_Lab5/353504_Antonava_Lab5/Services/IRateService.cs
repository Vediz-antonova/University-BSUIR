using _353504_Antonava_Lab5.Entities;
namespace _353504_Antonava_Lab5.Services;

public interface IRateService
{
    Task<IEnumerable<Rate>> GetRates(DateTime date);
}
