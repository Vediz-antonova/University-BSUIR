using System;

namespace task1
{
	class Program
	{
		static void Main(string[] args)
		{
			Bank bank = Bank.GetInstance();

			Depositor depositor1 = new Depositor("Alice", new Deposit(1000, new FixedInterestStrategy(0.05)));
			Depositor depositor2 = new Depositor("Bob", new Deposit(2000, new FixedInterestStrategy(0.03)));

			bank.AddDepositor(depositor1);
			bank.AddDepositor(depositor2);

			Console.WriteLine("Total deposits: " + bank.CalculateTotalDeposits());
			Console.WriteLine("Total interest: " + bank.CalculateTotalInterest());

			Depositor depositor3 = new BonusDepositor("Charlie", new Deposit(1500, new FixedInterestStrategy(0.04)), 100);
			bank.AddDepositor(depositor3);


			Console.ReadLine();
		}
	}
}
