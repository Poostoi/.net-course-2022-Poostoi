namespace Models;

public class ClientDb:Person
{
    public ClientDb()
    {
        Accounts = new List<AccountDb>();
    }

    public List<AccountDb> Accounts { get; set; }
    public int NumberPhone { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is not ClientDb)
            return false;
        var client = (ClientDb)obj;
        
        return client.Id == Id &&
               client.NumberPhone == NumberPhone &&
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