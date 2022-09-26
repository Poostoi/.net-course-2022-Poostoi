using Models;

namespace ModelsDb;

public class EmployeeDb: Person
{
    public EmployeeDb(Employee employee)
    {
        Surname = employee.Surname;
        Name = employee.Name;
        DateBirth = employee.DateBirth;
        PassportId = employee.PassportId;
        Bonus = employee.Bonus;
        Id = employee.Id;
        Salary = employee.Salary;
        Contract = employee.Contract;
    }
    public int Salary { get; set; }
    public string Contract { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is not EmployeeDb)
            return false;
        var employee = (EmployeeDb)obj;
        
        return employee.Id == Id &&
               employee.Contract == Contract &&
               employee.Surname == Surname &&
               employee.Name == Name &&
               employee.DateBirth == DateBirth &&
               employee.PassportId == PassportId &&
               employee.Salary == Salary;
    }

}