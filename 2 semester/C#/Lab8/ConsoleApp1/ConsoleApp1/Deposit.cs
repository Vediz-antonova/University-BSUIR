namespace task1
{
	public class Deposit
	{
		public double size { get; set; }
		private IInterestStrategy interestStrategy;

		public Deposit(double value, IInterestStrategy interestStrategy)
		{
			size = value;
			this.interestStrategy = interestStrategy;
		}

		public double CalculateInterest()
		{
			return interestStrategy.CalculateInterest(size);
		}
	}
}
