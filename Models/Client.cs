namespace Models;

public class Client:Person
{
    public Client()
    {
        Clients = new List<Client>();
    }

    public ICollection<Client> Clients { get; set; }
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

    public override int GetHashCode()
    {
        return NumberPhone.GetHashCode()+
               Name.GetHashCode()+
               Surname.GetHashCode()+
               DateBirth.GetHashCode()+
               PassportId.GetHashCode();
    }
}