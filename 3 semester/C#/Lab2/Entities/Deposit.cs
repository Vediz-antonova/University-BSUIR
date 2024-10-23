namespace _353504_Antonava_Lab2.Entities
{
	public class Deposit
	{
		public decimal Amount { get; set; }
		public InterestRateType InterestRate { get; set; }
		public decimal PeriodDays { get; set; } = 365;
	}
}
