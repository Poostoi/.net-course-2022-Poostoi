using Models;

namespace Services.Storage;

public class ClientStorage : IClientStorage
{
    public Dictionary<Client, Account> Data { get; }

    public ClientStorage()
    {
        Data = new Dictionary<Client, Account>();
    }

    public void AddAccount(Client client, Account account)
    {
        Data.Add(client, account);
    }

    public void UpdateAccount(Client client, Account account)
    {
        if (!Data.ContainsKey(client)) return;
        var existAccount = Data[client];
        existAccount.Amount = account.Amount;
        existAccount.Currency = account.Currency;
    }

    public void DeleteAccount(Client client, Account account)
    {
        if (!Data.ContainsKey(client)) return;
        Data.Remove(client);
    }

    public void Add(Client client)
    {
        Data.Add(client, new TestDataGenerator().GeneratingAccount());
    }

    public void Update(Client client)
    {
        var existClient = Data.First(c => c.Key.PassportId == client.PassportId).Key;
        existClient.NumberPhone = client.NumberPhone;
        existClient.Name = client.Name;
        existClient.Surname = client.Surname;
        existClient.DateBirth = client.DateBirth;
    }

    public void Delete(Client client)
    {
        if (!Data.ContainsKey(client)) return;
        Data.Remove(client);
    }
}