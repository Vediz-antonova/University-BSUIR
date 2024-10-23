using _353504_Antonava_Lab2.Collections;
using _353504_Antonava_Lab2.Contracts;
using Generic.Math;
using System;
using System.Collections.Generic;

namespace _353504_Antonava_Lab2.Entities
{
	public class BankSystem : IBankSystem
	{
		public event EventHandler<ClientEventArgs> ClientAdded;
		public event EventHandler<DepositEventArgs> DepositAdded;

		private MyCustomCollection<Client> clients = new MyCustomCollection<Client>();
		private Dictionary<InterestRateType, int> interestRates = new Dictionary<InterestRateType, int>
		{
			{ InterestRateType.Детский, 5 },
			{ InterestRateType.Пенсионный, 17 },
			{ InterestRateType.Стандартный, 20 }
		};

		public class ClientEventArgs : EventArgs
		{
			public Client Client { get; }
			public ClientEventArgs(Client client) => Client = client;
		}

		public class DepositEventArgs : EventArgs
		{
			public Client Client { get; }
			public decimal Amount { get; }
			public DepositEventArgs(Client client, decimal amount)
			{
				Client = client;
				Amount = amount;
			}
		}

		public void AddClient(Client client)
		{
			clients.Add(client);
			ClientAdded?.Invoke(this, new ClientEventArgs(client));
		}

		public void AddDeposit(int clientId, decimal amount, InterestRateType interest, short days)
		{
			var client = clients[clientId];
			client.Deposits.Add(new Deposit { Amount = amount, InterestRate = interest, PeriodDays = days });
			DepositAdded?.Invoke(this, new DepositEventArgs(client, amount));
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

		public void RemoveClient(Client client)
		{
			clients.Remove(client);
		}
	}
}
