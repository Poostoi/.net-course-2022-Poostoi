namespace Models;

public abstract class Person
{
    public Person(string surname, string name, int passportId, DateTime dateBirth)
    {
        Surname = surname;
        Name = name;
        PassportId = passportId;
        DateBirth = dateBirth;
    }

    public string Surname { get; set; }
    public string Name { get; set; }
    public int PassportId { get; set; }
    public DateTime DateBirth { get; set; }
    
    
}