using System.ComponentModel.Design;
using Models;
using Services.ExceptionCraft;
using Services.Filters;
using Services.Storage;

namespace Services;

public class ClientService
{
    private IClientStorage _clientStorage;
    public ClientService(IClientStorage clientStorage)
    {
        _clientStorage = clientStorage;
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
        request = request.ToDictionary(x=>x.Key,
            y=>y.Value);

        return (Dictionary<Client, Account>)request;
    }

}