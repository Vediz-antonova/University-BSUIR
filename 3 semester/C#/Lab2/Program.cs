using _353504_Antonava_Lab2.Entities;
using System;
using _353504_Antonava_Lab2.Collections;

namespace _353504_Antonava_Lab2
{
	class Program
	{
		static void Main(string[] args)
		{
			Journal journal = new Journal();
			BankSystem bankSystem = new BankSystem();

			bankSystem.ClientAdded += (sender, e) =>
			{
				journal.LogEvent($"Добавлен клиент: {e.Client.Name}");
			};

			bankSystem.DepositAdded += (sender, e) =>
			{
				journal.LogEvent($"Добавлен вклад на сумму {e.Amount} для клиента: {e.Client.Name}");
			};

			// Добавление клиентов
			var client1 = new Client { Name = "Клиент 1" };
			bankSystem.AddClient(client1);
			var client2 = new Client { Name = "Клиент 2" };
			bankSystem.AddClient(client2);

			// Добавление вкладов
			bankSystem.AddDeposit(0, 1000, InterestRateType.Детский, 365);
			bankSystem.AddDeposit(1, 2000, InterestRateType.Пенсионный, 365);

			// Вывод событий в журнале
			journal.DisplayEvents();

			// Обработка исключений
			try
			{
				bankSystem.RemoveClient(new Client { Name = "Неизвестный клиент" });
			}
			catch (ItemNotFoundException ex)
			{
				Console.WriteLine($"Ошибка: {ex.Message}");
			}

			Console.ReadLine();
		}
	}
}