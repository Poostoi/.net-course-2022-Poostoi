namespace Models;

public class Account
{
    public Account(){}
    public Guid Id { get; set; }
    public Currency Currency { get; set; }
    public int Amount { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Account)
            return false;
        var account = (Account)obj;

        return Currency.Code == account.Currency.Code &&
               Currency.Name == account.Currency.Name &&
               Amount == account.Amount;
    }
}