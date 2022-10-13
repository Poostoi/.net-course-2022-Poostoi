using System.ComponentModel.DataAnnotations;
using Models;

namespace ModelsDb;

public class AccountDb
{
    [Key] public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid CurrencyId { get; set; }
    public ClientDb Client { get; set; }
    public CurrencyDb Currency { get; set; }
    public int Amount { get; set; }
}