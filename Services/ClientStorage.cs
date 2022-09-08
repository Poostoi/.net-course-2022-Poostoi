
using Models;
using Services.ExceptionCraft;

namespace Services;

public class ClientStorage
{
    public readonly Dictionary<Client, Account> _clients = new Dictionary<Client, Account>();
    
    
    public void Add(Client client)
    {
        _clients.Add(client, new TestDataGenerator().GeneratingAccount());
    }

    public void Update()
    {
        
    }
    public void Remove()
    {
        
    }
}