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
    }

    public ClientService(BankContext bankContext)
    {
        _bankContext = bankContext;
    }

    public Client GetClient(Guid clientId)
    {
        var clientDb = _bankContext.Clients.FirstOrDefault(c => c.Id == clientId);

        return new Client()
        {
            Bonus = clientDb.Bonus,
            DateBirth = clientDb.DateBirth,
            Name = clientDb.Name,
            NumberPhone = clientDb.NumberPhone,
            PassportId = clientDb.PassportId,
            Surname = clientDb.Surname
        };
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
        clientInDatabase.AccountsDbs = new List<AccountDb>();
        clientInDatabase.NumberPhone = client.NumberPhone;
        clientInDatabase.Name = client.Name;
        clientInDatabase.Surname = client.Surname;
        clientInDatabase.DateBirth = client.DateBirth;
        clientInDatabase.Bonus = client.Bonus;
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

    public List<Client> GetClientsDb(ClientFilter clientFilter)
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
        var clientDbs = request.ToList();
        var clients = new List<Client>();
        foreach (var clientDb in clientDbs)
        {
            clients.Add(new Client()
            {
                Bonus = clientDb.Bonus,
                DateBirth = clientDb.DateBirth,
                Name = clientDb.Name,
                NumberPhone = clientDb.NumberPhone,
                PassportId = clientDb.PassportId,
                Surname = clientDb.Surname
            });
        }

        return clients;
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
}