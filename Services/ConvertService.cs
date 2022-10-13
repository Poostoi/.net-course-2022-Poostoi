using Models;
using ModelsDb;

namespace Services;

public class ConvertService
{
    public ClientDb ConvertClientInClientDb(Client client)
    {
        var clientDb = new ClientDb()
        {
            Bonus = client.Bonus,
            DateBirth = client.DateBirth,
            Id = client.Id,
            Name = client.Name,
            NumberPhone = client.NumberPhone,
            PassportId = client.PassportId,
            Surname = client.Surname
        };
        var listAccount = client.Accounts.Select(a => new AccountDb()
        {
            Amount = a.Amount,
            Client = clientDb,
            ClientId = a.ClientId,
            Currency = new CurrencyDb()
            {
                Code = a.Currency.Code,
                Id = a.Currency.Id,
                Name = a.Currency.Name
            }
        }).ToList();

        clientDb.AccountsDbs = listAccount;
        foreach (var accountDb in listAccount)
        {
            accountDb.Currency.AccountDbs = new List<AccountDb>()
            {
                accountDb
            };
        }

        return clientDb;
    }

    public Client ConvertClientDbInClient(ClientDb clientDb)
    {
        var client = new Client()
        {
            Bonus = clientDb.Bonus,
            DateBirth = clientDb.DateBirth,
            Id = clientDb.Id,
            Name = clientDb.Name,
            NumberPhone = clientDb.NumberPhone,
            PassportId = clientDb.PassportId,
            Surname = clientDb.Surname
        };
        var listAccount = client.Accounts.Select(a => new Account()
        {
            Amount = a.Amount,
            Client = client,
            ClientId = a.ClientId,
            Currency = new Currency()
            {
                Code = a.Currency.Code,
                Id = a.Currency.Id,
                Name = a.Currency.Name
            }
        }).ToList();

        client.Accounts = listAccount;
        foreach (var account in listAccount)
        {
            account.Currency.Accounts = new List<Account>()
            {
                account
            };
        }

        return client;
    }

    public Account ConvertAccountDbInAccount(AccountDb accountDb)
    {
        var account = new Account()
        {
            Id = accountDb.Id,
            Amount = accountDb.Amount,
            // Client = ConvertClientDbInClient(accountDb.Client),
            ClientId = accountDb.ClientId,
            // Currency = new Currency()
            // {
            //     Code = accountDb.Currency.Code,
            //     Id = accountDb.Currency.Id,
            //     Name = accountDb.Currency.Name
            // }
        };
        // var listAccount = accountDb.Currency.AccountDbs.Select(a => new Account()
        // {
        //     Amount = a.Amount,
        //     //  Client = ConvertClientDbInClient(a.Client),
        //     ClientId = a.ClientId,
        //     // Currency = new Currency()
        //     // {
        //     //     Code = a.Currency.Code,
        //     //     Id = a.Currency.Id,
        //     //     Name = a.Currency.Name,
        //     //     Accounts = new List<Account>()
        //     //     {
        //     //         account
        //     //     }
        //     // }
        // }).ToList();
        // account.Currency.Accounts = listAccount;

        return account;
    }

    public AccountDb ConvertAccountInAccountDb(Account account)
    {
        var accountDb = new AccountDb()
        {
            Amount = account.Amount,
            Client = ConvertClientInClientDb(account.Client),
            ClientId = account.ClientId,
            Currency = new CurrencyDb()
            {
                Code = account.Currency.Code,
                Id = account.Currency.Id,
                Name = account.Currency.Name
            }
        };
        var listAccount = account.Currency.Accounts.Select(a => new AccountDb()
        {
            Amount = a.Amount,
            Client = ConvertClientInClientDb(a.Client),
            ClientId = a.ClientId,
            Currency = new CurrencyDb()
            {
                Code = a.Currency.Code,
                Id = a.Currency.Id,
                Name = a.Currency.Name,
                AccountDbs = new List<AccountDb>()
                {
                    accountDb
                }
            }
        }).ToList();
        accountDb.Currency.AccountDbs = listAccount;
        return accountDb;
    }
}