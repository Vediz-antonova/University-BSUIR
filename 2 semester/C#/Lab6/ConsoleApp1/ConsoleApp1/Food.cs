using System;

namespace ConsoleApp1
{
	public sealed class Food : Product
	{
		public string ExpirationDate { get; set; } // Срок годности

		public Food(string name, double price, string expirationDate) : base(name, price)
		{
			ExpirationDate = expirationDate;
		}

		public override void DisplayInfo()
		{
			Console.WriteLine($"Food: {Name}, Price: {Price}, Expiration Date: {ExpirationDate}");
		}

		public override void ChangePrice(double newPrice)
		{
			base.ChangePrice(newPrice);
			Console.WriteLine($"New price for {Name} is {Price}");
		}
	}
}
