using System.ComponentModel.DataAnnotations;
using Models;

namespace ModelsDb;

public class CurrencyDb
{
    [Key] public Guid Id { get; set; }

    public List<AccountDb> AccountDbs { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }
}