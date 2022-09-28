using System.ComponentModel.DataAnnotations;
using Models;

namespace ModelsDb;

public class AccountDb
{
    public AccountDb()
    {
        Id = Guid.NewGuid();
        CurrencyDbs = new List<CurrencyDb>();
    }

    public AccountDb(Account account)
    {
        ClientDb = new ClientDb();
        Id = new Guid();
        CurrencyDbs = new List<CurrencyDb>();
        Amount = account.Amount;
    }

    public ClientDb ClientDb { get; set; }
    [Key]
    public Guid Id { get; set;  }
    public List<CurrencyDb> CurrencyDbs { get; set; }
    public int Amount { get; set; }
}