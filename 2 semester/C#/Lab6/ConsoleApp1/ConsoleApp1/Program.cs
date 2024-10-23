using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Product[] products = new Product[3];
			products[0] = new Food("Apple", 1.2, "2024-13-06");
			products[1] = new Book("War and Peace", 12.5, "Leo Tolstoy");
			products[2] = new Clothing("T-Shirt", 20.0, "M");

			while (true)
			{
				Console.WriteLine("1. Display product info");
				Console.WriteLine("2. Change product price");
				Console.WriteLine("3. Change clothing size");
				Console.WriteLine("4. Exit");
				Console.Write("Enter your choice: ");
				string input = Console.ReadLine();
				int choice;
				if (!int.TryParse(input, out choice) || choice < 1 || choice > 4)
				{
					Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
					continue;
				}

				if (choice == 4)
					break;

				Console.Write("Enter product index (0-2): ");
				input = Console.ReadLine();
				int index;
				if (!int.TryParse(input, out index) || index < 0 || index > 2)
				{
					Console.WriteLine("Invalid index. Please enter a number between 0 and 2.");
					continue;
				}

				switch (choice)
				{
					case 1:
						products[index].DisplayInfo();
						break;
					case 2:
						Console.Write("Enter new price: ");
						input = Console.ReadLine();
						double newPrice;
						if (!double.TryParse(input, out newPrice) || newPrice < 0)
						{
							Console.WriteLine("Invalid price. Please enter a positive number.");
							continue;
						}
						products[index].ChangePrice(newPrice);
						break;
					case 3:
						if (products[index] is Clothing clothing)
						{
							Console.Write("Enter new size: ");
							string newSize = Console.ReadLine();
							if (string.IsNullOrEmpty(newSize))
							{
								Console.WriteLine("Invalid size. Please enter a non-empty string.");
								continue;
							}
							clothing.ChangeSize(newSize);
						}
						else
						{
							Console.WriteLine("This product is not a clothing item.");
						}
						break;
				}
			}
		}
	}
}
