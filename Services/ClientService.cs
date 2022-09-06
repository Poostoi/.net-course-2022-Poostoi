using System.ComponentModel.Design;
using Models;
using Services.ExceptionCraft;

namespace Services;

public class ClientService
{
    private Dictionary<Client, Account> _clients = new Dictionary<Client, Account>();

    public void AddAccount(Client client, Account account)
    {
        if (_clients.ContainsKey(client))
            throw new IsKeyExistInDictionaryException("Такой клиент уже существует");
        if (DateTime.Now.Year - client.DateBirth.Year < 18)
            throw new AgeLessException("Возраст меньше 18.");
        if (client.PassportId == 0)
            throw new NotPassportDataException("У клиента нет пасспортных данных.");
        _clients.Add(client, account);
    }
}