using System.ComponentModel.DataAnnotations;
using Models;

namespace ModelsDb;

public class AccountDb
{
    [Key] public Guid Id { get; set; }

    public ClientDb ClientDb { get; set; }
    public CurrencyDb CurrencyDb { get; set; }
    public int Amount { get; set; }
}