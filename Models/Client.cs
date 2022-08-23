namespace Models;

public class Client:Person
{
    public Client(string surname, string name, int passportId, DateTime dateBirth) : base(surname, name, passportId, dateBirth)
    {
    }
}