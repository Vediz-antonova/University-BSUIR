namespace task1
{
	public class BonusDepositor : Depositor
	{
		private double bonus;

		public BonusDepositor(string value, Deposit deposit_value, double bonus) : base(value, deposit_value)
		{
			this.bonus = bonus;
		}

		public double CalculateInterest()
		{
			return base.deposit.CalculateInterest() + bonus;
		}
	}
}

