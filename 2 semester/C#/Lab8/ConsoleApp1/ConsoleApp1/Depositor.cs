namespace task1
{
	public class Depositor
	{
		public string name { get; set; }
		public Deposit deposit { get; set; }

		public Depositor(string value, Deposit deposit_value)
		{
			name = value;
			deposit = deposit_value;
		}
	}
}
