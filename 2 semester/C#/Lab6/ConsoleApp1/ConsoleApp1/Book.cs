using System;

namespace ConsoleApp1
{
	public class Book : Product
	{
		public string Author { get; set; }

		public Book(string name, double price, string author) : base(name, price)
		{
			Author = author;
		}

		public override void DisplayInfo()
		{
			Console.WriteLine($"Book: {Name}, Price: {Price}");
		}

		public new void DisplayInfoWithAuthor()
		{
			Console.WriteLine($"Book: {Name}, Price: {Price}, Author: {Author}");
		}

		public override void ChangePrice(double newPrice)
		{
			base.ChangePrice(newPrice);
			Console.WriteLine($"New price for {Name} is {Price}");
		}
	}
}
