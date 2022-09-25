namespace Models;

public class CurrencyDb
{
    public CurrencyDb()
    {
        Id = new Guid();
    }

    public List<AccountDb> Accounts { get; set; }
    public Guid Id { get; private init; }
    public int Code { get; set; }
    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not CurrencyDb)
            return false;
        var currency = (CurrencyDb)obj;

        return Id == currency.Id &&
               Code == currency.Code &&
               Name == currency.Name;
    }
}