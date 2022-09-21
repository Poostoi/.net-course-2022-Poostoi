using Microsoft.EntityFrameworkCore;
using Models;

namespace Migration;

public class BankContext : DbContext
{
    public DbSet<Client> Clients { get; set; }

    protected BankContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("" +
                                 "Host=localhost;" +
                                 "Port = 5433;" +
                                 "Database = postgres;" +
                                 "Username = postgres;" +
                                 "Password = 37242");
    }
}