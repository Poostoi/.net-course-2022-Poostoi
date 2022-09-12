using Models;

namespace Services.Storage;

public interface IClientStorage:IStorage<Client>
{
    public Dictionary<Client,Account> Data { get; }

    public void AddAccount(Client client, Account account);

    public void UpdateAccount(Client client, Account account);

    public void RemoveAccount(Client client, Account account);

}