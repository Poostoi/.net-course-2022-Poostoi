namespace Models;

public class Employee: Person
{
    public Employee(string surname, string name, int passportId, DateTime dateBirth) : base(surname, name, passportId, dateBirth)
    {
    }
    public string Contract { get; set; }
    
}