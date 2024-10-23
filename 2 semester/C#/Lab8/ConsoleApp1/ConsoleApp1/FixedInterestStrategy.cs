namespace task1
{
	public class FixedInterestStrategy : IInterestStrategy
	{
		private double interestRate;

		public FixedInterestStrategy(double interestRate)
		{
			this.interestRate = interestRate;
		}

		public double CalculateInterest(double depositSize)
		{
			return depositSize * interestRate;
		}
	}
}
