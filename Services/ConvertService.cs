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
}