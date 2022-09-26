using Models;

namespace ModelsDb;

public class ClientDb : Person
{
    public ClientDb()
    {
        AccountsDbs = new List<AccountDb>();
    }

    public ClientDb(Client client)
    {
        Surname = client.Surname;
        Name = client.Name;
        DateBirth = client.DateBirth;
        PassportId = client.PassportId;
        Bonus = client.Bonus;
        Id = client.Id;
        NumberPhone = client.NumberPhone;
        AccountsDbs = new List<AccountDb>();
    }
    
    public List<AccountDb> AccountsDbs { get; set; }
    public int NumberPhone { get; set; }
}