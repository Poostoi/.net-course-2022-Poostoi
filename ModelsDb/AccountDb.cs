namespace ModelsDb;

public class AccountDb
{
    public AccountDb()
    {
        Id = new Guid();
        CurrencyDbs = new List<CurrencyDb>();
    }

    public ClientDb ClientDb { get; set; }
    public Guid Id { get; private init;  }
    public List<CurrencyDb> CurrencyDbs { get; set; }
    public int Amount { get; set; }
}