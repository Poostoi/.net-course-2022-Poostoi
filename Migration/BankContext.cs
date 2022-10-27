using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Models;
using ModelsDb;

namespace Migration;

public class BankContext : DbContext
{
    public DbSet<ClientDb> Clients { get; set; }
    public DbSet<EmployeeDb> Employees { get; set; }
    public DbSet<AccountDb> Accounts { get; set; }
    public DbSet<CurrencyDb> Currencies { get; set; }
    public BankContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;" +
                                 "Port = 5432;" +
                                 "Database = postgres;" +
                                 "Username = postgres;" +
                                 "Password = 37242");
        
    }
    
}