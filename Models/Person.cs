namespace Models;

public abstract class Person
{
    public int Bonus { get; set; }

    public string Surname { get; set; }
    public string Name { get; set; }
    public int PassportId { get; set; }
    public DateTime DateBirth { get; set; }
    
    
}