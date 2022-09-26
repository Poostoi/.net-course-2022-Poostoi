namespace Models;

public class Account
{
    

    public Client Client { get; set; }
    
    public int Amount { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Account)
            return false;
        var account = (Account)obj;
        
        return Client.Equals(account.Client)&&
               Amount == account.Amount;
    }
}