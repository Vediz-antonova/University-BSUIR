using System;
using System.Collections.Generic;
using System.Linq;
using _353504_Antonava_Lab3.Contracts;

namespace _353504_Antonava_Lab3.Entities
{
	public class BankSystem : IBankSystem
	{
		// События
		public event EventHandler<ClientAddedEventArgs> ClientAdded;
		public event EventHandler<DepositAddedEventArgs> DepositAdded;

		public record ClientAddedEventArgs(Client Client);
		public record DepositAddedEventArgs(Client Client, Deposit Deposit);

		// Коллекции для хранения клиентов и тарифов
		private List<Client> clients = new List<Client>();
		private Dictionary<InterestRateType, decimal> interestRates = new Dictionary<InterestRateType, decimal>
		{
			{ InterestRateType.Студенческий, 5 },
			{ InterestRateType.Пенсионный, 17 },
			{ InterestRateType.Стандартный, 20 }
		};

		public void AddClient(Client client)
		{
			clients.Add(client);
			ClientAdded?.Invoke(this, new ClientAddedEventArgs(client));
		}

		public void AddDeposit(Client client, decimal amount, InterestRateType interest, int days)
		{
			var deposit = new Deposit { Amount = amount, InterestRate = interest, PeriodDays = days };
			client.Deposits.Add(deposit);
			DepositAdded?.Invoke(this, new DepositAddedEventArgs(client, deposit));
		}

		public decimal CalculateClientInterest(int clientId, int depositId)
		{
			var client = clients[clientId];
			Deposit dep = client.Deposits[depositId];
			return dep.Amount * interestRates[dep.InterestRate] / 100 * dep.PeriodDays / 365;
		}

		public decimal CalculateTotalInterest()
		{
			return clients.Sum(client => client.Deposits.Sum(deposit => CalculateClientInterest(clients.IndexOf(client), client.Deposits.IndexOf(deposit))));
		}

		public decimal CalculateTotalDeposits()
		{
			return clients.Sum(client => client.Deposits.Sum(deposit => deposit.Amount));
		}

		public List<string> GetSortedInterestRates()
		{
			return interestRates.OrderBy(rate => rate.Value)
							   .Select(rate => rate.Key.ToString())
							   .ToList();
		}

		// Запросы LINQ
		public class ClientInterestResult
		{
			public string ClientName { get; set; }
			public decimal TotalInterest { get; set; }
		}

		public class MaxInterestClientResult
		{
			public string Name { get; set; }
			public decimal TotalInterest { get; set; }
		}

		public class TotalDepositResult
		{
			public string ClientName { get; set; }
			public decimal TotalDeposit { get; set; }
		}

		public IEnumerable<ClientInterestResult> GetClientInterestTotals()
		{
			return from client in clients
				   select new ClientInterestResult
				   {
					   ClientName = client.Name,
					   TotalInterest = client.Deposits.Sum(deposit => CalculateClientInterest(clients.IndexOf(client), client.Deposits.IndexOf(deposit)))
				   };
		}

		public IEnumerable<string> GetClientsAboveThreshold(decimal threshold)
		{
			return from client in clients
				   let totalInterest = client.Deposits.Sum(deposit => CalculateClientInterest(clients.IndexOf(client), client.Deposits.IndexOf(deposit)))
				   where totalInterest > threshold
				   select client.Name;
		}

		public MaxInterestClientResult GetMaxInterestClient()
		{
			return (from client in clients
					let totalInterest = client.Deposits.Sum(deposit => CalculateClientInterest(clients.IndexOf(client), client.Deposits.IndexOf(deposit)))
					orderby totalInterest descending
					select new MaxInterestClientResult
					{
						Name = client.Name,
						TotalInterest = totalInterest
					})
					.FirstOrDefault();
		}

		public IEnumerable<TotalDepositResult> GetTotalDepositsByClient()
		{
			return clients
				.GroupBy(client => client.Name)
				.Select(group => new TotalDepositResult
				{
					ClientName = group.Key,
					TotalDeposit = group.SelectMany(c => c.Deposits).Sum(deposit => deposit.Amount)
				});
		}
	}
}