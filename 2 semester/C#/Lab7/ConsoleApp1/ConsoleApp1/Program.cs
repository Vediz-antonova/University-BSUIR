using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Polynomial p1 = new Polynomial(1, 2, 3);
			Polynomial p2 = new Polynomial(4, 5, 6);

			Console.WriteLine("p1: " + p1);
			Console.WriteLine("p2: " + p2);

			Polynomial p3 = p1 + p2;
			Console.WriteLine("p1 + p2: " + p3);

			Polynomial p4 = p1 - p2;
			Console.WriteLine("p1 - p2: " + p4);

			Polynomial p5 = p1 * 2;
			Console.WriteLine("p1 * 2: " + p5);

			Polynomial p6 = p1 / 2;
			Console.WriteLine("p1 / 2: " + p6);

			Console.WriteLine("p1 == p2: " + (p1 == p2));
			Console.WriteLine("p1 != p2: " + (p1 != p2));

			Console.WriteLine("p1 is true: " + (p1 ? "true" : "false"));

			int a = (int)p1;
			Console.WriteLine("p1 to int: " + a);

			Polynomial p7 = (Polynomial)a;
			Console.WriteLine("int to Polynomial: " + p7);

			Console.ReadLine();
		}

	}
}