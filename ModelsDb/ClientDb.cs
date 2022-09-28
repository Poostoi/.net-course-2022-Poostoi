using Models;

namespace ModelsDb;

public class ClientDb : PersonDb
{
    public List<AccountDb> AccountsDbs { get; set; }
    public int NumberPhone { get; set; }
}