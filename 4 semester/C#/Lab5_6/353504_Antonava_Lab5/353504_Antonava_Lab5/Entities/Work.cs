using SQLite;
namespace _353504_Antonava_Lab5.Entities;

[Table("Works")]
public class Work
{
    [PrimaryKey, AutoIncrement, Indexed]
    public int WorkId { get; set; }

    public string Name { get; set; }

    public string Details { get; set; }

    [Indexed]
    public int BrigadeId { get; set; }
}