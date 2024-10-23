using System;

namespace ConsoleApp1
{
	public class Clothing : Product
	{
		public string Size { get; set; }

		public Clothing(string name, double price, string size) : base(name, price)
		{
			Size = size;
		}

		public override void DisplayInfo()
		{
			Console.WriteLine($"Clothing: {Name}, Price: {Price}, Size: {Size}");
		}

		public void ChangeSize(string newSize)
		{
			Size = newSize;
			Console.WriteLine($"New size for {Name} is {Size}");
		}
	}
}
