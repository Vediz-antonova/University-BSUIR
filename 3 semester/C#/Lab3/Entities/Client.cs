namespace _353504_Antonava_Lab3.Entities
{
	public class Client
	{
		public string Name { get; set; }
		internal List<Deposit> Deposits { get; set; } = new List<Deposit>();
	}
}