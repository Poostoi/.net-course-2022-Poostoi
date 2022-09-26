using Migration;
using Models;
using ModelsDb;
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

    public void AddAccount(Client client)
    {
        if (DateTime.Now.Year - client.DateBirth.Year < 18)
            throw new AgeLessException("Возраст меньше 18.");
        if (client.PassportId == 0)
            throw new NotPassportDataException("У клиента нет пасспортных данных.");
        _clientStorage.Add(client);
    }

    public Dictionary<Client, Account> GetClients(ClientFilter clientFilter)
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

        return (Dictionary<Client, Account>)request;
    }

    public Client GetClient(Guid clientId)
    {
        return _bankContext.Clients.FirstOrDefault(c => c.Id == clientId);
    }

    public void AddClient(Client client)
    {
        _bankContext.Clients.Add(client);
        _bankContext.SaveChanges();
    }

    public void AddAccountDb(Guid clientId)
    {
        GetClient(clientId).Accounts.Add( new TestDataGenerator().GeneratingAccount());
    }

    public void ChangeClientDb(Guid clientId, Client client)
    {
        var clientInDatabase = GetClient(clientId);
        clientInDatabase.Accounts = client.Accounts;
        clientInDatabase.NumberPhone = client.NumberPhone;
        clientInDatabase.Name = client.Name;
        clientInDatabase.Surname = client.Surname;
        clientInDatabase.DateBirth = client.DateBirth;
        clientInDatabase.Bonus = client.Bonus;
        clientInDatabase.PassportId = client.PassportId;
        _bankContext.SaveChanges();
    }

    public void RemoveClientDb(Guid clientDbId)
    {
        _bankContext.ClientsDb.Remove(GetClient(clientDbId));
        _bankContext.SaveChanges();
    }
    public void RemoveAccountDb(Client client, Account account)
    {
        GetClient(client.Id).Accounts.Remove(account);
        _bankContext.SaveChanges();
    }
    public List<Client> GetClientsDb(ClientFilter clientFilter)
    {
        var request = _bankContext.ClientsDb.Select(c => c);

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