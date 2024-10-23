using _353504_Antonava_Lab3.Entities;
using System;

namespace _353504_Antonava_Lab3
{
	class Program
	{
		static void Main(string[] args)
		{
			BankSystem bankSystem = new BankSystem();

			// Подписка на события
			bankSystem.ClientAdded += (sender, e) =>
			{
				Console.WriteLine($"Клиент добавлен: {e.Client.Name}");
			};

			bankSystem.DepositAdded += (sender, e) =>
			{
				Console.WriteLine($"Вклад добавлен для клиента {e.Client.Name}: {e.Deposit.Amount} с тарифом {e.Deposit.InterestRate}");
			};

			// Пример добавления клиентов и вкладов
			Client client1 = new Client { Name = "Иван" };
			bankSystem.AddClient(client1);
			bankSystem.AddDeposit(client1, 1000, InterestRateType.Студенческий, 30);
			bankSystem.AddDeposit(client1, 2000, InterestRateType.Стандартный, 30);

			Client client2 = new Client { Name = "Петр" };
			bankSystem.AddClient(client2);
			bankSystem.AddDeposit(client2, 1500, InterestRateType.Пенсионный, 30);

			// Вызов методов и вывод результатов
			Console.WriteLine("Список тарифов, отсортированных по величине процента:");
			var sortedRates = bankSystem.GetSortedInterestRates();
			foreach (var rate in sortedRates)
			{
				Console.WriteLine(rate);
			}

			Console.WriteLine($"Общая величина процентов всех вкладов: {bankSystem.CalculateTotalInterest()}");
			Console.WriteLine($"Общая величина всех вкладов: {bankSystem.CalculateTotalDeposits()}");

			// Запросы LINQ
			// 1. Общие проценты по каждому клиенту
			var clientInterestTotals = bankSystem.GetClientInterestTotals();
			Console.WriteLine("Общие проценты по каждому клиенту:");
			foreach (var item in clientInterestTotals)
			{
				Console.WriteLine($"{item.ClientName}: {item.TotalInterest}");
			}

			// 2. Клиенты, заплатившие больше определенной суммы
			decimal threshold = 100m;
			var clientsAboveThreshold = bankSystem.GetClientsAboveThreshold(threshold);
			Console.WriteLine($"Клиенты, заплатившие больше {threshold}:");
			foreach (var name in clientsAboveThreshold)
			{
				Console.WriteLine(name);
			}

			// 3. Клиент с максимальными процентами
			var maxInterestClient = bankSystem.GetMaxInterestClient();
			if (maxInterestClient != null)
			{
				Console.WriteLine($"Клиент с максимальными процентами: {maxInterestClient.Name}, Сумма процентов: {maxInterestClient.TotalInterest}");
			}

			// 4. Суммы вкладов по каждому клиенту
			var totalDepositsByClient = bankSystem.GetTotalDepositsByClient();
			Console.WriteLine("Суммы вкладов по каждому клиенту:");
			foreach (var item in totalDepositsByClient)
			{
				Console.WriteLine($"{item.ClientName}: {item.TotalDeposit}");
			}
		}
	}
}