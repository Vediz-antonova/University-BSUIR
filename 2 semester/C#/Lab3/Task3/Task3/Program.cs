using System;
using Services;

class Program
{
    static void Main()
    {
        Console.WriteLine("1 - Day of Week \"dd.MM.yyyy\"\n2 - Number of Days");
        var x = Convert.ToInt16(Console.ReadLine());
        switch (x)
        {
            case 1:
                var s = Console.ReadLine(); 
                Console.WriteLine(DateServices.GetDay(s));
                break;
            case 2:
                Console.WriteLine("Input day: ");
                var day = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input month: ");
                var month = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input year: ");
                var year = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(DateServices.GetDaysSpan(day, month, year));
                break;
        }
    }
}