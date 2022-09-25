using Models;

namespace Services.Storage;

public interface IClientStorage : IStorage<ClientDb>
{
    public Dictionary<ClientDb,AccountDb> Data { get; }

    public void AddAccount(ClientDb clientDb, AccountDb accountDb);

    public void UpdateAccount(ClientDb clientDb, AccountDb accountDb);

    public void DeleteAccount(ClientDb clientDb, AccountDb accountDb);

}