namespace Classes
{
    public class Bank
    {
        private static Bank _instance;
        public string Name { get; set; }
        public int DepositCount { get; private set; }
        public double DepositSize { get; private set; }

        private Bank()
        {
            Name = "";
            DepositCount = 0;
            DepositSize = 0;
        }

        // Метод для получения экземпляра класса
        public static Bank GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Bank();
            }

            return _instance;
        }

        // Метод для добавления вклада
        public void AddDeposit(Deposit deposit)
        {
            DepositCount++;
            DepositSize += deposit.Amount;
        }

        // Обновление после внесенния изменений
        public void UpdateDepositSize(double change)
        {
            DepositSize += change;
        }
        
        public void UpdateDepositSize()
        {
            DepositSize += 100;
        }
        
        // Метод для подсчета общей выплаты по процентам
        public double CalculateTotalInterest()
        {
            return Deposit.CalculateInterest(DepositSize);
        }
    }
}