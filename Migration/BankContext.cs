using Microsoft.EntityFrameworkCore;
using Models;
using ModelsDb;

namespace Migration;

public class BankContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public BankContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port = 5432;Database = postgres;Username = postgres;Password = 37242");
    }
    
}