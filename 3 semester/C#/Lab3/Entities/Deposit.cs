namespace _353504_Antonava_Lab3.Entities
{
	public class Deposit
	{
		public decimal Amount { get; set; }
		public InterestRateType InterestRate { get; set; }
		public int PeriodDays { get; set; } = 365;
	}
}
