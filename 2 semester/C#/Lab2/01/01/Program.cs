using System;
using System.Globalization;

namespace _01
{
    internal class Program
    {
        static void SolutionToEqation()
        {
            Console.Write("ax^2 + bx + c = 0\n");
            double a, b, c;

            Console.Write("Enter a: ");
            while (!double.TryParse(Console.ReadLine(), out a))
            {
                Console.Write("Invalid input. Please enter a valid number for a: ");
            }
            Console.Write("Enter b: ");
            while (!double.TryParse(Console.ReadLine(), out b))
            {
                Console.Write("Invalid input. Please enter a valid number for b: ");
            }
            Console.Write("Enter c: ");
            while (!double.TryParse(Console.ReadLine(), out c))
            {
                Console.Write("Invalid input. Please enter a valid number for c: ");
            }

            double x1, x2, D;
            if (a == 0.0 && b == 0.0 && c == 0.0)
            {
                Console.Write("Infinite number of solutions");
            }
            else if (a == 0.0)
            {
                if (b == 0.0)
                {
                    Console.Write("There are no roots");
                }
                else
                {
                    if (c != 0.0)
                    {
                        x1 = (-c / b);
                        Console.Write("x = " + x1);
                    }
                    else Console.Write("0");
                }
            }
            else
            {
                D = b * b - 4 * a * c;

                if (D > 0.0)
                {
                    x1 = (-b + Math.Sqrt(D)) / (2 * a);
                    x2 = (-b - Math.Sqrt(D)) / (2 * a);

                    Console.Write("x1 = " + x1 + " x2 = " + x2);
                }
                else if (D == 0.0)
                {
                    x1 = -b / (2 * a);

                    Console.Write("x = " + x1);
                }
                else Console.Write("Complex");
            }
        }

        public static void Main(string[] args)
        {
            SolutionToEqation();
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
                        SolutionToEqation();
                        break;

                    case 2:
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
    }
}