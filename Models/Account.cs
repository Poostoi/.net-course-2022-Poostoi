namespace Models;

public class Account
{
    public Account()
    {
        Id = new Guid();
    }

    public Client Client { get; set; }
    public Guid Id { get; private init; }
    public Currency Currency { get; set; }
    public int Amount { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Account)
            return false;
        var account = (Account)obj;
        return Currency.Equals(account.Currency) && Amount == account.Amount;
    }
}