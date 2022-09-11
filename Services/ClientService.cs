using System.ComponentModel.Design;
using Models;
using Services.ExceptionCraft;
using Services.Filters;

namespace Services;

public class ClientService
{
    private ClientStorage _clientStorage;
    public ClientService(ClientStorage clientStorage)
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
        var dictionary = new Dictionary<Client, Account>();
        IEnumerable<KeyValuePair<Client, Account>> request = null;
        if (clientFilter.Name != null && clientFilter.Name != "")
            request = _clientStorage._clients.Where(c => 
                c.Key.Name == clientFilter.Name);
        if (clientFilter.Surname != null && clientFilter.Surname != "")
            request = _clientStorage._clients.Where(c => 
                c.Key.Surname == clientFilter.Surname);
        if (clientFilter.NumberPhone != 0)
            request = _clientStorage._clients.Where(c => 
                c.Key.NumberPhone == clientFilter.NumberPhone);
        if (clientFilter.PassportId != 0)
            request = _clientStorage._clients.Where(c => 
                c.Key.PassportId == clientFilter.PassportId);
        if (clientFilter.DateStart != new DateTime())
            request = _clientStorage._clients.Where(c => 
                c.Key.DateBirth >= clientFilter.DateStart);
        if (clientFilter.DateEnd != new DateTime())
            request = _clientStorage._clients.Where(c => 
                c.Key.DateBirth <= clientFilter.DateEnd);
        dictionary = request.ToDictionary(x=>x.Key,
            y=>y.Value);;

        return dictionary;
    }

}