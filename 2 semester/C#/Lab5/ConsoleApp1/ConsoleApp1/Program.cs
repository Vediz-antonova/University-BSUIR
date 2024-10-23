using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Bank bank = new Bank();

			Depositor dep1 = new Depositor { Name = "Иван", Dep = new Deposit(500, DepositType.Small) };
			Depositor dep2 = new Depositor { Name = "Анна", Dep = new Deposit(1000, DepositType.Medium) };

			bank.Depositors.Add(dep1);
			bank.Depositors.Add(dep2);

			Console.WriteLine(bank.CalculateTotalInterest());
		}
	}
}
