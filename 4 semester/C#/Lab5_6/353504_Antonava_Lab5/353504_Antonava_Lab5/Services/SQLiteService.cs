using SQLite;
using _353504_Antonava_Lab5.Entities;
namespace _353504_Antonava_Lab5.Services;

public class SQLiteService : IDbService
{
    private readonly SQLiteConnection _connection;

    public SQLiteService()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "appdb.db3");
        _connection = new SQLiteConnection(dbPath);
        Init();
    }

    public void Init()
    {
        //_connection.DropTable<Brigade>();
        //_connection.DropTable<Work>();
        _connection.CreateTable<Brigade>();
        _connection.CreateTable<Work>();

        if (!_connection.Table<Brigade>().Any())
        {
            var brigades = new List<Brigade>
            {
                new Brigade { Name = "Бригада А", Description = "Капитальный ремонт" },
                new Brigade { Name = "Бригада Б", Description = "Электромонтажные работы" },
                new Brigade { Name = "Бригада В", Description = "Отделочные работы" },
                new Brigade { Name = "Бригада Г", Description = "Специализированные работы" }
            };

            foreach (var brigade in brigades)
            {
                _connection.Insert(brigade);
            }

            Random random = new Random();

            foreach (var brigade in brigades)
            {
                int count = random.Next(5, 11);
                for (int i = 1; i <= count; i++)
                {
                    _connection.Insert(new Work
                    {
                        Name = $"Работа {i} для {brigade.Name}",
                        Details = $"Описание работы {i} для {brigade.Name}",
                        BrigadeId = brigade.Id
                    });
                }
            }
        }
    }

    public IEnumerable<Brigade> GetAllBrigades()
    {
        return _connection.Table<Brigade>().ToList();
    }

    public IEnumerable<Work> GetWorksByBrigade(int brigadeId)
    {
        return _connection.Table<Work>().Where(w => w.BrigadeId == brigadeId).ToList();
    }
}
