using _353504_Antonava_Lab1.Collections;

namespace _353504_Antonava_Lab1.Entities
{
	public class Client
	{
		public string Name { get; set; }
		internal MyCustomCollection<Deposit> Deposits { get; set; } = new MyCustomCollection<Deposit>();
	}
}
