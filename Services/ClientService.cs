using Migration;
using Models;
using Services.ExceptionCraft;
using Services.Filters;
using Services.Storage;

namespace Services;

public class ClientService
{
    private IClientStorage _clientStorage;
    private BankContext _bankContext;

    public ClientService(IClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
    }public ClientService(BankContext bankContext)
    {
        _bankContext = bankContext;
    }

    public void AddAccount(ClientDb clientDb)
    {
        if (DateTime.Now.Year - clientDb.DateBirth.Year < 18)
            throw new AgeLessException("Возраст меньше 18.");
        if (clientDb.PassportId == 0)
            throw new NotPassportDataException("У клиента нет пасспортных данных.");
        _clientStorage.Add(clientDb);
    }

    public Dictionary<ClientDb, AccountDb> GetClients(ClientFilter clientFilter)
    {
        var request = _clientStorage.Data.Select(c => c);

        if (clientFilter.Name != null && clientFilter.Name != "")
            request = request.Where(c =>
                c.Key.Name == clientFilter.Name);
        if (clientFilter.Surname != null && clientFilter.Surname != "")
            request = request.Where(c =>
                c.Key.Surname == clientFilter.Surname);
        if (clientFilter.NumberPhone != 0)
            request = request.Where(c =>
                c.Key.NumberPhone == clientFilter.NumberPhone);
        if (clientFilter.PassportId != 0)
            request = request.Where(c =>
                c.Key.PassportId == clientFilter.PassportId);

        if (clientFilter.DateStart != new DateTime())
            request = request.Where(c =>
                c.Key.DateBirth >= clientFilter.DateStart);
        if (clientFilter.DateEnd != new DateTime())
            request = request.Where(c =>
                c.Key.DateBirth <= clientFilter.DateEnd);
        request = request.ToDictionary(x => x.Key,
            y => y.Value);

        return (Dictionary<ClientDb, AccountDb>)request;
    }

    public ClientDb GetClientDb(Guid clientId)
    {
        return _bankContext.Clients.FirstOrDefault(c => c.Id == clientId);
    }

    public void AddClientDb(ClientDb clientDb)
    {
        _bankContext.Clients.Add(clientDb);
        _bankContext.SaveChanges();
    }

    public void AddAccountDb(Guid clientId)
    {
        GetClientDb(clientId).Accounts.Add( new TestDataGenerator().GeneratingAccount());
    }

    public void ChangeClientDb(Guid clientId, ClientDb clientDb)
    {
        var clientInDatabase = GetClientDb(clientId);
        clientInDatabase.Accounts = clientDb.Accounts;
        clientInDatabase.NumberPhone = clientDb.NumberPhone;
        clientInDatabase.Name = clientDb.Name;
        clientInDatabase.Surname = clientDb.Surname;
        clientInDatabase.DateBirth = clientDb.DateBirth;
        clientInDatabase.Bonus = clientDb.Bonus;
        clientInDatabase.PassportId = clientDb.PassportId;
        _bankContext.SaveChanges();
    }

    public void RemoveClientDb(Guid clientDbId)
    {
        _bankContext.Clients.Remove(GetClientDb(clientDbId));
        _bankContext.SaveChanges();
    }
    public void RemoveAccountDb(ClientDb clientDb, AccountDb accountDb)
    {
        GetClientDb(clientDb.Id).Accounts.Remove(accountDb);
        _bankContext.SaveChanges();
    }
    public List<ClientDb> GetClientsDb(ClientFilter clientFilter)
    {
        var request = _bankContext.Clients.Select(c => c);

        if (clientFilter.Name != null && clientFilter.Name != "")
            request = request.Where(c =>
                c.Name == clientFilter.Name);
        if (clientFilter.Surname != null && clientFilter.Surname != "")
            request = request.Where(c =>
                c.Surname == clientFilter.Surname);
        if (clientFilter.NumberPhone != 0)
            request = request.Where(c =>
                c.NumberPhone == clientFilter.NumberPhone);
        if (clientFilter.PassportId != 0)
            request = request.Where(c =>
                c.PassportId == clientFilter.PassportId);

        if (clientFilter.DateStart != new DateTime())
            request = request.Where(c =>
                c.DateBirth >= clientFilter.DateStart);
        if (clientFilter.DateEnd != new DateTime())
            request = request.Where(c =>
                c.DateBirth <= clientFilter.DateEnd);

        return request.ToList();
    }
}