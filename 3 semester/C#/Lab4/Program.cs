using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _353504_Antonava_Lab4
{
	class Program
	{
		static void Main(string[] args)
		{
			string folderName = "Antonava_Lab4";
			if (Directory.Exists(folderName))
			{
				Directory.Delete(folderName, true);
				Console.WriteLine("Содержимое папки удалено.");
			}
			
			Directory.CreateDirectory(folderName);
			Console.WriteLine($"Папка будет создана по пути: {Path.GetFullPath(folderName)}");

			// Создание 10 пустых файлов
			string[] extensions = { ".txt", ".rtf", ".dat", ".inf" };
			Random rand = new Random();
			for (int i = 0; i < 10; i++)
			{
				string fileName = Path.Combine(folderName, Path.GetRandomFileName() + extensions[rand.Next(extensions.Length)]);
				File.Create(fileName).Dispose();
			}

			// Вывод списка файлов
			var files = Directory.GetFiles(folderName);
			foreach (var file in files)
			{
				Console.WriteLine($"Файл: {Path.GetFileName(file)} имеет расширение {Path.GetExtension(file)}");
			}

			// Создание и заполнение коллекции объектов Car
			var cars = new List<Car>
			{
				new Car(1, false, "Toyota"),
				new Car(2, true, "Land Rover"),
				new Car(3, false, "GMC"),
				new Car(4, true, "Hyundai"),
				new Car(5, false, "Chevrolet")
			};

			// Запись коллекции в файл
			string dataFileName = Path.Combine(folderName, "cars.dat");
			var fileService = new FileService();
			fileService.SaveData(cars, dataFileName);

			// Переименование файла
			string newDataFileName = Path.Combine(folderName, "renamed_cars.dat");
			File.Move(dataFileName, newDataFileName);

			// Чтение данных из файла
			var loadedCars = new List<Car>(fileService.ReadFile(newDataFileName));

			// Сортировка с помощью MyCustomComparer
			loadedCars.Sort(new MyCustomComparer());
			Console.WriteLine("Отсортированная коллекция:");
			foreach (var car in loadedCars)
			{
				Console.WriteLine($"{car.Name} (ID: {car.Id}, Автоматическая коробка передач: {car.IsAutomaticTransmission})");
			}

			// Дополнительная сортировка по Id
			var sortedById = loadedCars.OrderBy(c => c.Id);
			Console.WriteLine("Отсортированная коллекция по ID:");
			foreach (var car in sortedById)
			{
				Console.WriteLine($"{car.Name} (ID: {car.Id})");
			}

			Console.ReadLine();
		}
	}
}