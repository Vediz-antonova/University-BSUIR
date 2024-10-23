using System;
namespace _353504_Antonava
{
    internal class Program
    {
        public static void Main (string[] args)
        {
            Console.Write("Enter a: ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter b: ");
            double b = Convert.ToDouble(Console.ReadLine());

            double x = a / b;
            double? y = b / a;
            Console.WriteLine("a/b = " + x + "\nb/a = " + y);
        }
    }
}