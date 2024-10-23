using System;

namespace Services
{
    public class Function
    {
        public void CalculateAndPrint(double z)
        {
            double x;
            double y;
            int branchNumber;

            if (z < -1)
            {
                x = -z / 3;
                branchNumber = 1;
            }
            else
            {
                x = Math.Abs(z);
                branchNumber = 2;
            }

            y = Math.Log(x + 0.5) + (Math.Exp(x) - Math.Exp(-x));

            Console.WriteLine($"Result: y = {y}");
            Console.WriteLine($"Branch number: {branchNumber}");
        }
    }
}