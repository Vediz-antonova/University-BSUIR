using System;

namespace _02
{
    internal class Program
    {
        static readonly int a = 123;
        static void Graph()
        {
            
            Console.Write("Enter x: ");
            double x;
            while (!double.TryParse(Console.ReadLine(), out x))
            {
                Console.Write("Invalid input. Please enter a valid number for x: ");
            }
            Console.Write("Enter y: ");
            double y;
            while (!double.TryParse(Console.ReadLine(), out y))
            {
                Console.Write("Invalid input. Please enter a valid number for y: ");
            }

            if (x * x + y * y < 64.0 && x * x + y * y > 9.0 && x < 0.0)
            {
                Console.Write("Yes");
            }
            else if ((x * x + y * y == 64.0 || x * x + y * y == 9.0) && x <= 0 || (x * x + y * y <= 64.0 && x * x + y * y >= 9.0 && x == 0))
            {
                Console.Write("On border");
            }
            else
            {
                Console.Write("No");
            }
        }

        public static void Main(string[] args)
        {
            Console.Write(a);
            Graph();
            while (true)
            {
                Console.Write("\nDo you want to complete: " +
                              "\n1) Yes" +
                              "\n2) No");
                short complete;
                while (!short.TryParse(Console.ReadLine(), out complete) || (complete != 1 && complete != 2))
                {
                    Console.Write("Invalid input. Please enter 1 for Yes or 2 for No: ");
                }

                switch (complete) {
                    case 1:
                        Graph();
                        break;

                    case 2:
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
    }
}