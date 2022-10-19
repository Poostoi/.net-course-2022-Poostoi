namespace Models;

public class Currency
{
    public List<Account>? Accounts { get; set; }
    public Guid Id { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Currency)
            return false;
        var currency = (Currency)obj;

        return Code == currency.Code &&
               Name == currency.Name;
    }
}