using Models;

namespace Services.Storage;

public class IClientStorage:IStorage<Client>
{
    public Dictionary<Client,Account> Data { get; }
    public  void Add(Client item)
    {
        throw new NotImplementedException();
    }

    public void Update(Client item)
    {
        throw new NotImplementedException();
    }

    public void Remove(Client item)
    {
        throw new NotImplementedException();
    }
    public  void AddAccount(Client item, Account account)
    {
        throw new NotImplementedException();
    }

    public void UpdateAccount(Client item, Account account)
    {
        throw new NotImplementedException();
    }

    public void RemoveAccount(Client item, Account account)
    {
        throw new NotImplementedException();
    }
    
}