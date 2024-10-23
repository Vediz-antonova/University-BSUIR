namespace task1;

public class Deposit
{
    public double DepositSize { get; set; }
    public DepositType Type { get; set; }

    public double InterestRate
    {
        get
        {
            switch (Type)
            {
                case DepositType.Small:
                    return 5;
                case DepositType.Medium:
                    return 10;
                case DepositType.Large:
                    return 20;
                default:
                    return 0;
            }
        }
    }

    public Deposit(double size, DepositType type)
    {
        DepositSize = size;
        Type = type;
    }

    public void ChangeSize(double change)
    {
        DepositSize += change;
    }

    public double CalculateInterest()
    {
        return DepositSize * InterestRate / 100;
    }
}