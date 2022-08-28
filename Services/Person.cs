namespace Services;

public abstract class Person
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public int PassportId { get; set; }
    public DateTime DateBirth { get; set; }
}