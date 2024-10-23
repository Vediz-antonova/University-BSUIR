using System;

namespace ConsoleApp1
{
	public abstract class Product
	{
		private string name;
		private double price;

		public string Name
		{
			get { return name; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentException("Name cannot be null or empty.");
				name = value;
			}
		}

		public double Price
		{
			get { return price; }
			set
			{
				if (value < 0)
					throw new ArgumentException("Price cannot be negative.");
				price = value;
			}
		}

		public Product(string name, double price)
		{
			Name = name;
			Price = price;
		}

		public abstract void DisplayInfo();

		public virtual void ChangePrice(double newPrice)
		{
			Price = newPrice;
		}
	}
}