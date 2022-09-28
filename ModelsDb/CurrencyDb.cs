using System.ComponentModel.DataAnnotations;
using Models;

namespace ModelsDb;

public class CurrencyDb
{
    public CurrencyDb()
    {
        Id = Guid.NewGuid();
    }

    public CurrencyDb(Currency currency)
    {
        AccountDbs = new List<AccountDb>();
        Id = currency.Id;;
        Code = currency.Code;
        Name = currency.Name;
    }

    public List<AccountDb> AccountDbs { get; set; }
    [Key]
    public Guid Id { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }
   
}