namespace ModelsDb;

public class CurrencyDb
{
    public CurrencyDb()
    {
        Id = new Guid();
    }

    public List<AccountDb> AccountDbs { get; set; }
    public Guid Id { get; private init; }
    public int Code { get; set; }
    public string Name { get; set; }
   
}