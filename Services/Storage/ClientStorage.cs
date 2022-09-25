using Models;

namespace Services.Storage;

public class ClientStorage : IClientStorage
{
    public Dictionary<ClientDb, AccountDb> Data { get; }

    public ClientStorage()
    {
        Data = new Dictionary<ClientDb, AccountDb>();
    }

    public void AddAccount(ClientDb clientDb, AccountDb accountDb)
    {
        Data.Add(clientDb, accountDb);
    }

    public void UpdateAccount(ClientDb clientDb, AccountDb accountDb)
    {
        if (!Data.ContainsKey(clientDb)) return;
        var existAccount = Data[clientDb];
        existAccount.Amount = accountDb.Amount;
        existAccount.CurrenciesDb = accountDb.CurrenciesDb;
    }

    public void DeleteAccount(ClientDb clientDb, AccountDb accountDb)
    {
        if (!Data.ContainsKey(clientDb)) return;
        Data.Remove(clientDb);
    }

    public void Add(ClientDb clientDb)
    {
        Data.Add(clientDb, new TestDataGenerator().GeneratingAccount());
    }

    public void Update(ClientDb clientDb)
    {
        var existClient = Data.First(c => c.Key.PassportId == clientDb.PassportId).Key;
        existClient.NumberPhone = clientDb.NumberPhone;
        existClient.Name = clientDb.Name;
        existClient.Surname = clientDb.Surname;
        existClient.DateBirth = clientDb.DateBirth;
    }

    public void Delete(ClientDb clientDb)
    {
        if (!Data.ContainsKey(clientDb)) return;
        Data.Remove(clientDb);
    }
}