namespace Services;

public class BankService
{
    public double CalculationBankOwnerSalary(double bankProfit,
                                            double expenses,
                                            int  numberBankOwners) =>
        (bankProfit-expenses)/numberBankOwners;

    public Employee TransformationClientInEmployee(Client client) => 
        new Employee()
        {
            Surname = client.Surname,
            Name = client.Name,
            DateBirth = client.DateBirth,
            PassportId = client.PassportId,
            Salary = 0
        };
}