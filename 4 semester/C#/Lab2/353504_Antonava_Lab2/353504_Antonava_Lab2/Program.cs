using CarCenter;
using Service;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

public class Program
{
	public static async Task Main(string[] args)
	{
		Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: Начало работы");

		var cars = new List<Car>();
		for (int i = 0; i < 1000; i++)
		{
			cars.Add(new Car(i, $"Car {i}", DateTime.Now.AddDays(i)));
		}

		var streamService = new StreamService<Car>();
		var progress = new Progress<string>(message => Console.WriteLine(message));
		var stream = new MemoryStream();

		var writeTask = Task.Run(() => streamService.WriteToStreamAsync(stream, cars, progress));
		Thread.Sleep(200); 
		var copyTask = Task.Run(() => streamService.CopyFromStreamAsync(stream, "cars.json", progress));

		Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: Методы WriteToStreamAsync и CopyFromStreamAsync запущены");

		await Task.WhenAll(writeTask, copyTask);

		var count = await streamService.GetStatisticsAsync("cars.json", car => car.InspectionDate > DateTime.Now);
		Console.WriteLine($"Количество машин с актуальной датой техосмотра: {count}");
	}
}