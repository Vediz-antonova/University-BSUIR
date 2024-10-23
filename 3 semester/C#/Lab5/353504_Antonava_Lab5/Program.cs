using System;
using _353504_Antonava_Lab5.Domain;
using Microsoft.Extensions.Configuration; 

class Program
{
	static void Main(string[] args)
	{
		// Чтение конфигурации
		var config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		string fileName = config["FileName"];

		// Создание коллекции объектов
		List<Runway> runways = new List<Runway>
		{
			new Runway { Name = "Runway 1", Length = 3000, Width = 60 },
			new Runway { Name = "Runway 2", Length = 2500, Width = 45 },
			new Runway { Name = "Runway 3", Length = 3200, Width = 50 },
			new Runway { Name = "Runway 4", Length = 2800, Width = 55 },
			new Runway { Name = "Runway 5", Length = 3100, Width = 65 },
			new Runway { Name = "Runway 6", Length = 2700, Width = 40 }
		};

		// Создание экземпляра сериализатора
		ISerializer serializer = new Serializer();

		// Сериализация в три формата
		serializer.SerializeXML(runways, $"{fileName}.xml");
		serializer.SerializeJSON(runways, $"{fileName}.json");
		serializer.SerializeByLINQ(runways, $"{fileName}_linq.xml");

		// Десериализация
		var xmlRunways = serializer.DeSerializeXML($"{fileName}.xml");
		var jsonRunways = serializer.DeSerializeJSON($"{fileName}.json");
		var linqRunways = serializer.DeSerializeByLINQ($"{fileName}_linq.xml"); 

		// Проверка данных на совпадение
		bool xmlMatch = CompareRunways(runways, xmlRunways);
		bool jsonMatch = CompareRunways(runways, jsonRunways);
		bool linqMatch = CompareRunways(runways, linqRunways); 

		// Вывод результатов
		Console.WriteLine($"XML data match: {xmlMatch}");
		Console.WriteLine($"JSON data match: {jsonMatch}");
		Console.WriteLine($"LINQ data match: {linqMatch}"); 

		Console.ReadKey();
	}

	// Метод для сравнения коллекций Runway
	private static bool CompareRunways(List<Runway> original, IEnumerable<Runway> deserialized)
	{
		if (original.Count != deserialized.Count())
			return false;

		for (int i = 0; i < original.Count; i++)
		{
			if (!original[i].Equals(deserialized.ElementAt(i)))
			{
				return false;
			}
		}
		return true;
	}
}