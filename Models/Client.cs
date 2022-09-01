namespace Models;

public class Client:Person
{
    public int NumberPhone { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is not Client)
            return false;
        var client = (Client)obj;
        
        return client.NumberPhone == NumberPhone &&
               client.Surname == Surname &&
               client.Name == Name &&
               client.DateBirth == DateBirth &&
               client.PassportId == PassportId;
    }
}