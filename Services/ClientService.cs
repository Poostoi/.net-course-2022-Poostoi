using System.ComponentModel.Design;
using Models;
using Services.Exception;

namespace Services;

public class ClientService
{
    private Dictionary<Client,Account> Clients { get; set; }
    public void AddClients(Client client, Account account)
    {
        try
        {
            if (Clients.ContainsKey(client))
                throw new ArgumentException("Такой клиент уже существует");
            if (DateTime.Now.Year - client.DateBirth.Year < 18)
                throw new AgeLessException("Возраст меньше 18.");
            if (client.PassportId == 0)
                throw new ClientNotPassportDataException("У клиента нет пасспортных данных.");
            Clients.Add(client, account);
        }
        catch (AgeLessException e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }
    }
}