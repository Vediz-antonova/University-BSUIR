using _353504_Antonava_Lab3.Entities;

namespace _353504_Antonava_Lab3.Contracts
{
	public interface IBankSystem
	{
		void AddClient(Client client);
		void AddDeposit(Client client, decimal amount, InterestRateType interest, int days);
		List<string> GetSortedInterestRates();
		decimal CalculateTotalInterest();
		decimal CalculateTotalDeposits();
	}
}