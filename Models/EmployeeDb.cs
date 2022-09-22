namespace Models;

public class Employee: Person
{
    public int Salary { get; set; }
    public string Contract { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is not Employee)
            return false;
        var employee = (Employee)obj;
        
        return employee.Contract == Contract &&
               employee.Surname == Surname &&
               employee.Name == Name &&
               employee.DateBirth == DateBirth &&
               employee.PassportId == PassportId &&
               employee.Salary == Salary;
    }

}