namespace Classes
{
    public class Deposit
    {
        public double Amount { get; private set; }
        public static double InterestRate { get; set; } // Процентная ставка

        public Deposit(double amount)
        {
            Amount = amount;
        }

        // Метод для изменения размера вклада
        public void ChangeAmount(double change)
        {
            Amount += change;
        }

        // Перегруженный метод для увеличения вклада
        public void ChangeAmount()
        {
            Amount += 100; // Увеличение на стандартную сумму
        }

        // Статический метод для расчета выплаты по процентам
        public static double CalculateInterest(double amount)
        {
            return amount * InterestRate / 100;
        }
    }
}