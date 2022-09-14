using Models;

namespace Services;

public class BankService<T> where T : Person
{
    public List<Person> BlackList { get; }

    public BankService()
    {
        BlackList = new List<Person>();
    }
    
    public int CalculationBankOwnerSalary(int bankProfit,
        int expenses,
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

    public void AddBonus(T person) => person.Bonus++;

    public void AddToBlackList<P>(P person) where P : Person
    {
        BlackList.Add(person);
    }
    
    public bool IsPersonInBlackList<P>(P person) where P : Person => BlackList.Contains(person);

}