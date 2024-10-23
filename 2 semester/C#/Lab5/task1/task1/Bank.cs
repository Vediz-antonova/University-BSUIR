namespace task1;

public class Bank
{
    public string Name { get; set; }
    public List<Depositor> Depositors { get; set; }

    public Bank()
    {
        Name = "";
        Depositors = new List<Depositor>();
    }
        
    public double CalculateTotalInterest()
    {
        double totalInterest = 0;
        foreach (var depositor in Depositors)
        {
            totalInterest += depositor.Dep.CalculateInterest();
        }
        return totalInterest;
    }
}
