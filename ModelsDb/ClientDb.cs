using Models;

namespace ModelsDb;

public class ClientDb : Person
{
    public ClientDb()
    {
        AccountsDbs = new List<AccountDb>();
    }
    public List<AccountDb> AccountsDbs { get; set; }
    public int NumberPhone { get; set; }
}