using _353504_Antonava_Lab5.Entities;
namespace _353504_Antonava_Lab5.Services;

public interface IDbService
{
    IEnumerable<Brigade> GetAllBrigades();
    IEnumerable<Work> GetWorksByBrigade(int brigadeId);
    void Init();
}