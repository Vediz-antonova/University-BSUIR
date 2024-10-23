using System.Collections.Generic;

namespace task1
{
	public class Bank
	{
		private static Bank _instance;
		public string name { get; set; }
		public List<Depositor> depositors { get; set; }

		private Bank()
		{
			name = "";
			depositors = new List<Depositor>();
		}

		public static Bank GetInstance()
		{
			if (_instance == null)
			{
				_instance = new Bank();
			}

			return _instance;
		}

		public void AddDepositor(Depositor depositor)
		{
			depositors.Add(depositor);
		}

		public double CalculateTotalDeposits()
		{
			double total = 0;
			foreach (var depositor in depositors)
			{
				total += depositor.deposit.size;
			}
			return total;
		}

		public double CalculateTotalInterest()
		{
			double total = 0;
			foreach (var depositor in depositors)
			{
				total += depositor.deposit.CalculateInterest();
			}
			return total;
		}
	}
}
