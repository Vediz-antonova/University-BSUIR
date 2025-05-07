using System.Text.Json.Serialization;
namespace _353504_Antonava_Lab5.Entities;

public class Rate
{
    [JsonPropertyName("Cur_ID")]
    public int Cur_ID { get; set; }

    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("Cur_Abbreviation")]
    public string Cur_Abbreviation { get; set; }

    [JsonPropertyName("Cur_Scale")]
    public int Cur_Scale { get; set; }

    [JsonPropertyName("Cur_Name")]
    public string Cur_Name { get; set; }

    [JsonPropertyName("Cur_OfficialRate")]
    public decimal Cur_OfficialRate { get; set; }
}
