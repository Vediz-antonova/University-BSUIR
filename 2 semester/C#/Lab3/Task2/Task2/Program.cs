using System;
using Services;

class Program
{
    static void Main()
    {
        var calculate = new Function();

        Console.WriteLine("Enter z:");
        double z;
        while (!Double.TryParse(Console.ReadLine(), out z))
        {
            Console.Write("Invalid input. Please enter a valid number for z: ");
        }

        calculate.CalculateAndPrint(z);
    }
}