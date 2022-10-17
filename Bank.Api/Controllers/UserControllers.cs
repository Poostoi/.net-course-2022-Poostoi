using Microsoft.AspNetCore.Mvc;
using Migration;
using Models;
using Services;

namespace BankApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserControllers : ControllerBase
{
    public ClientService _clientService;

    public UserControllers()
    {
        _clientService = new ClientService(new BankContext());
    }

    [HttpGet]
    public Client GetClient(Guid Id)
    {
        var client = _clientService.GetClient(Id);
        return client;
    }

    [HttpPost]
    public void AddClient(Client client)
    {
        _clientService.AddClient(client);
    }

    [HttpDelete]
    public void DeleteClient(Guid id)
    {
        _clientService.RemoveClient(id);
    }
    [HttpPut]
    public void UpdateClient(Guid id, Client client)
    {
        _clientService.ChangeClient(id,client);
    }
}