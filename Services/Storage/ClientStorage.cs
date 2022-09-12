using Models;
using Services.Storage;

namespace Services;

public class ClientStorage : IClientStorage
{
    public Dictionary<Client, Account> Data { get; }
    public ClientStorage() => Data = new Dictionary<Client, Account>();
    public void AddAccount(Client client, Account account) => Data.Add(client, account);

    public void UpdateAccount(Client client, Account account) =>  throw new NotImplementedException();

    public void RemoveAccount(Client client, Account account) =>  throw new NotImplementedException();


    public void Add(Client client) => Data.Add(client, new TestDataGenerator().GeneratingAccount());


    public void Update(Client client) =>  throw new NotImplementedException();

    public void Delete(Client client) =>  throw new NotImplementedException();
}