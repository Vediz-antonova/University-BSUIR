using _353504_Antonava_Lab1.Entities;
using System;

namespace _353504_Antonava_Lab1
{
	class Program
	{
		static void Main(string[] args)
		{
			BankSystem bankSystem = new BankSystem();

			// Добавление клиентов
			bankSystem.AddClient(new Client());
			bankSystem.AddClient(new Client());

			// Добавление вкладов
			bankSystem.AddDeposit(0, 1000, InterestRateType.Детский, 365);
			Console.WriteLine("0 клиенту добавлен вклад 1000 на 1 год с процентной ставкой детской");
			bankSystem.AddDeposit(0, 2000, InterestRateType.Пенсионный, 365);
			Console.WriteLine("0 клиенту добавлен вклад 2000 на 1 год с процентной ставкой пенсионной");
			bankSystem.AddDeposit(1, 1500, InterestRateType.Стандартный, 365);
			Console.WriteLine("1 клиенту добавлен вклад 1500 на 1 год с процентной ставкой стандартной");

			// Вычисление общей суммы выплат по процентам
			decimal totalInterest = bankSystem.CalculateTotalInterest();
			Console.WriteLine($"Общая сумма выплат по процентам: {totalInterest}");

			// Вычисление процентов клиента по конкретному вкладу
			decimal clientInterest = bankSystem.CalculateClientInterest(0, 0);
			Console.WriteLine($"Проценты клиента 0 по счету 0: {clientInterest}");

			Console.ReadLine();
		}
	}
}
