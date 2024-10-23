using System;

public class Square
{
    public static int f(int n)
    {
        return n * n;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine($"2^2 + 3^2 + 4^2 = {Square.f(2) + Square.f(3) + Square.f(4)}");
        
        Console.WriteLine("Enter n:");
        int n;
        while (!Int32.TryParse(Console.ReadLine(), out n))
        {
            Console.Write("Invalid input. Please enter a valid number for n: ");
        }
        Console.WriteLine($"{n}^2 = {Square.f(n)}");
    }
}
