namespace Models;

public abstract class Person
{
    protected Person()
    {
        Id = new Guid();
    }

    public Guid Id { get; private init; }
    public int Bonus { get; set; }

    public string Surname { get; set; }
    public string Name { get; set; }
    public int PassportId { get; set; }
    public DateTime DateBirth { get; set; }
    
    
}