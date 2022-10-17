using AutoMapper;
using Microsoft.EntityFrameworkCore;
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


    public async Task<Client> GetClient(Guid clientId)
    {
        var clientDb = await _bankContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);
        if (clientDb == null) return null;
        return _mapperService.MapperFromClientDbInClient.Map<Client>(clientDb);
    }

    public async Task AddClient(Client client)
    {
        await _bankContext.Clients.AddAsync(_mapperService.MapperFromClientInClientDb.Map<ClientDb>(client));
        await _bankContext.SaveChangesAsync();
    }

    public void AddAccount(Client client)
    {
        if (DateTime.Now.Year - client.DateBirth.Year < 18)
            throw new AgeLessException("Возраст меньше 18.");
        if (client.PassportId == 0)
            throw new NotPassportDataException("У клиента нет пасспортных данных.");
        _clientStorage.Add(client);
    }

    public async Task AddAccount(Guid clientId, Account account)
    {
        var clientDb = await _bankContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);
        var accountDb = _mapperService.MapperFromAccountInAccountDb.Map<AccountDb>(account);
        accountDb.Client = clientDb;
        clientDb.AccountsDbs.Add(accountDb);
        _bankContext.Update(clientDb);
        await _bankContext.SaveChangesAsync();
    }

    public async Task ChangeClient(Guid clientId, Client client)
    {
        var clientInDatabase = await _bankContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);
        clientInDatabase.NumberPhone = client.NumberPhone;
        clientInDatabase.Bonus = client.Bonus;
        clientInDatabase.Name = client.Name;
        clientInDatabase.Surname = client.Surname;
        clientInDatabase.DateBirth = client.DateBirth;
        clientInDatabase.PassportId = client.PassportId;
        _bankContext.Update(clientInDatabase);
        await _bankContext.SaveChangesAsync();
    }

    public async Task RemoveClient(Guid clientId)
    {
        _bankContext.Clients.Remove(_bankContext.Clients.FirstOrDefault(c => c.Id == clientId));
        await _bankContext.SaveChangesAsync();
    }

    public async Task RemoveAccount(Client client, Account account)
    {
        var accountDb = _mapperService.MapperFromAccountInAccountDb.Map<AccountDb>(account);
        var clientDb = await _bankContext.Clients.FirstOrDefaultAsync(c => c.Id == client.Id);
        clientDb.AccountsDbs.Remove(accountDb);
        await _bankContext.SaveChangesAsync();
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

        return request.Select(clientDb => _mapperService.MapperFromClientDbInClient.Map<Client>(clientDb))
            .ToList();
    }

    public List<Client> GetClientsLimit(ClientFilter clientFilter, int count)
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
        var list = request.Select(clientDb => _mapperService.MapperFromClientDbInClient.Map<Client>(clientDb))
            .ToList();

        return list.GetRange(0, count);
    }

    public async Task UpdateAccount(Account newAccount)
    {
        var oldAccount = await _bankContext.Accounts.FirstOrDefaultAsync(c => c.Id == newAccount.Id);
        oldAccount.Amount = newAccount.Amount;
        _bankContext.Update(oldAccount);
        await _bankContext.SaveChangesAsync();
    }
}