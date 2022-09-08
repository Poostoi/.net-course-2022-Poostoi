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
    public void AddAccount(Client client, Account account)
    {
        if (DateTime.Now.Year - client.DateBirth.Year < 18)
            throw new AgeLessException("Возраст меньше 18.");
        if (client.PassportId == 0)
            throw new NotPassportDataException("У клиента нет пасспортных данных.");
        _clientStorage.Add(client);
        _clientStorage._clients.FirstOrDefault();
    }

    public Dictionary<Client, Account> GetClients(ClientFilter clientFilter)
    {
       // if (clientFilter.Name != "")
            return new Dictionary<Client, Account>();
    }

}