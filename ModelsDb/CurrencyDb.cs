using Models;

namespace ModelsDb;

public class CurrencyDb
{
    public CurrencyDb()
    {
        Id = new Guid();
    }

    public CurrencyDb(Currency currency)
    {
        AccountDbs = new List<AccountDb>();
        Id = new Guid();;
        Code = currency.Code;
        Name = currency.Name;
    }

    public List<AccountDb> AccountDbs { get; set; }
    public Guid Id { get; protected set; }
    public int Code { get; set; }
    public string Name { get; set; }
   
}