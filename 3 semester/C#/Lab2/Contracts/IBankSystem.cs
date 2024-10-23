using _353504_Antonava_Lab2.Collections;

namespace _353504_Antonava_Lab2.Contracts
{
	public interface IBankSystem
	{
		void AddDeposit(int clientId, decimal amount, InterestRateType interest, short days);
		decimal CalculateTotalInterest();
		decimal CalculateClientInterest(int clientId, int depositId);
	}
}
