using AutoMapper;
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
    private MapperService _mapperService;

    public ClientService(IClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
    }

    public ClientService(BankContext bankContext)
    {
        _bankContext = bankContext;
        _mapperService = new MapperService();
    }

    

    public Client GetClient(Guid clientId)
    {
        var clientDb = _bankContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (clientDb == null) return null;
        return _mapperService.MapperFromClientDbInClient.Map<Client>(clientDb);
    }

    public void AddClient(Client client)
    {
        _bankContext.Clients.Add(new ClientDb(client));
        _bankContext.SaveChanges();
    }

    public void AddAccount(Client client)
    {
        if (DateTime.Now.Year - client.DateBirth.Year < 18)
            throw new AgeLessException("Возраст меньше 18.");
        if (client.PassportId == 0)
            throw new NotPassportDataException("У клиента нет пасспортных данных.");
        _clientStorage.Add(client);
    }

    public void AddAccount(Guid clientId)
    {
        var clientDb = _bankContext.Clients.FirstOrDefault(c => c.Id == clientId);
        clientDb.AccountsDbs.Add(new AccountDb(new TestDataGenerator().GeneratingAccount()));
        _bankContext.Update(clientDb);
        _bankContext.SaveChanges();
    }

    public void ChangeClient(Guid clientId, Client client)
    {
        var clientInDatabase = _bankContext.Clients.FirstOrDefault(c => c.Id == clientId);
        clientInDatabase.NumberPhone = client.NumberPhone;
        clientInDatabase.Bonus = client.Bonus;
        clientInDatabase.Name = client.Name;
        clientInDatabase.Surname = client.Surname;
        clientInDatabase.DateBirth = client.DateBirth;
        clientInDatabase.PassportId = client.PassportId;
        _bankContext.Update(clientInDatabase);
        _bankContext.SaveChanges();
    }

    public void RemoveClient(Guid clientId)
    {
        _bankContext.Clients.Remove(_bankContext.Clients.FirstOrDefault(c => c.Id == clientId));
        _bankContext.SaveChanges();
    }

    public void RemoveAccountDb(Client client, Account account)
    {
        _bankContext.Clients.FirstOrDefault(c => c.Id == client.Id).AccountsDbs.Remove(new AccountDb(account));
        _bankContext.SaveChanges();
    }

    public List<Client> GetClients(ClientFilter clientFilter)
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

        return request.Select(clientDb => _mapperService.MapperFromClientDbInClient .Map<Client>(clientDb))
            .ToList();
    }

}