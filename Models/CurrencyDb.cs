namespace Models;

public class Currency
{
    public Currency()
    {
        Id = new Guid();
    }

    public ICollection<AccountDb> Accounts { get; set; }
    public Guid Id { get; private init; }
    public int Code { get; set; }
    public string Name { get; set; }
}