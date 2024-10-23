using System;
using Classes;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter name of bank: ");
        string s = Console.ReadLine(); 
        Bank bank = Bank.GetInstance();
        bank.Name = s;

        Console.WriteLine("Enter InterestRate: ");
        double Is;
        while (!Double.TryParse(Console.ReadLine(), out Is))
        {
            Console.Write("Invalid input. Please enter a valid number for InterestRate: ");
        }
        Deposit.InterestRate = Is; 

        Deposit deposit1 = new Deposit(-10000); 
        Deposit deposit2 = new Deposit(-15000); 

        bank.AddDeposit(deposit1);
        bank.AddDeposit(deposit2);

        Console.WriteLine($"Общая выплата по процентам: {bank.CalculateTotalInterest()}");

        Console.WriteLine("Enter change amount of deposit1: ");
        int change;
        while (!Int32.TryParse(Console.ReadLine(), out change))
        {
            Console.Write("Invalid input. Please enter a valid number for ChangeAmount: ");
        }
        deposit1.ChangeAmount(change); 
        deposit2.ChangeAmount(); 
        bank.UpdateDepositSize(change);
        bank.UpdateDepositSize();

        Console.WriteLine($"Общая выплата по процентам после изменения вкладов: {bank.CalculateTotalInterest()}");
    }
}