using SQLite;
namespace _353504_Antonava_Lab5.Entities;

[Table("Brigades")]
public class Brigade
{
    [PrimaryKey, AutoIncrement, Indexed]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
