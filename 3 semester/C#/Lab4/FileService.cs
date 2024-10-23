using System;
using System.Collections.Generic;
using System.IO;

namespace _353504_Antonava_Lab4
{
	public class FileService : IFileService<Car>
	{
		public IEnumerable<Car> ReadFile(string fileName)
		{
			using (var reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
			{
				while (reader.BaseStream.Position != reader.BaseStream.Length)
				{
					Car car = null; // Создаем переменную для хранения объекта Car
					try
					{
						int id = reader.ReadInt32();
						bool isAutomaticTransmission = reader.ReadBoolean();
						string name = reader.ReadString();

						car = new Car(id, isAutomaticTransmission, name); // Инициализируем объект Car
					}
					catch (EndOfStreamException)
					{
						break; // Выход из цикла при достижении конца файла
					}
					catch (IOException ex)
					{
						Console.WriteLine($"Ошибка чтения из файла: {ex.Message}");
						yield break; // Завершение метода в случае ошибки
					}

					if (car != null) // Проверяем, что объект был успешно создан
					{
						yield return car; // Возвращаем объект Car
					}
				}
			}
		}

		public void SaveData(IEnumerable<Car> data, string fileName)
		{
			if (File.Exists(fileName))
			{
				File.Delete(fileName);
			}

			using (var writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
			{
				try
				{
					foreach (var car in data)
					{
						writer.Write(car.Id);
						writer.Write(car.IsAutomaticTransmission);
						writer.Write(car.Name);
					}
				}
				catch (IOException ex)
				{
					Console.WriteLine($"Ошибка записи в файл: {ex.Message}");
				}
			}
		}
	}
}