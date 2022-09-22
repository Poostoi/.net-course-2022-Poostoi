﻿using Microsoft.EntityFrameworkCore;
using Models;

namespace Migration;

public class BankContext : DbContext
{
    public DbSet<ClientDb> Clients { get; set; }
    public DbSet<EmployeeDb> Employees { get; set; }
    public DbSet<AccountDb> Accounts { get; set; }
    public DbSet<CurrencyDb> Currencies { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port = 5432;Database = postgres;Username = postgres;Password = 1111");
    }
}