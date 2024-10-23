using _353504_Antonava_Lab1.Collections;
using _353504_Antonava_Lab1.Contracts;
using Generic.Math;
using System.Collections.Generic;

namespace _353504_Antonava_Lab1.Entities
{
	public class BankSystem : IBankSystem
	{
		private MyCustomCollection<Client> clients = new MyCustomCollection<Client>();
		private Dictionary<InterestRateType, int> interestRates = new Dictionary<InterestRateType, int>
		{
			{ InterestRateType.Детский, 5 },
			{ InterestRateType.Пенсионный, 17 },
			{ InterestRateType.Стандартный, 20 }
		};

		public void AddClient(Client client)
		{
			clients.Add(client);
		}

		public void AddDeposit(int clientId, decimal amount, InterestRateType interest, short days)
		{
			var client = clients[clientId];
			client.Deposits.Add(new Deposit { Amount = amount, InterestRate = interest, PeriodDays = days});
		}

		public decimal CalculateTotalInterest()
		{
			decimal totalInterest = 0;
			for (int i = 0; i < clients.Count; i++)
			{
				for (int j = 0; j < clients[i].Deposits.Count; j++)
				{
					totalInterest = GenericMath.Add(totalInterest, CalculateClientInterest(i, j));
				}
			}
			return totalInterest;
		}

		public decimal CalculateClientInterest(int clientId, int depositId)
		{
			var client = clients[clientId];
			Deposit dep = client.Deposits[depositId];
			return dep.Amount * interestRates[dep.InterestRate] / 100 * dep.PeriodDays / 365;
		}
	}
}
