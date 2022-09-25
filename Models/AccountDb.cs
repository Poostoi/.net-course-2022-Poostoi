namespace Models;

public class AccountDb
{
    public AccountDb()
    {
        Id = new Guid();
        CurrenciesDb = new List<CurrencyDb>();
    }

    public ClientDb ClientDb { get; set; }
    public Guid Id { get; private init;  }
    public List<CurrencyDb> CurrenciesDb { get; set; }
    public int Amount { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not AccountDb)
            return false;
        var account = (AccountDb)obj;
        
        return Id == account.Id &&
               ClientDb.Equals(account.ClientDb)&&
               Amount == account.Amount;
    }
}