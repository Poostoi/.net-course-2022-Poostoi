using Microsoft.AspNetCore.Mvc;
using Migration;
using Models;
using Services;

namespace BankApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientControllers : ControllerBase
{
    public ClientService _clientService;

    public ClientControllers()
    {
        _clientService = new ClientService(new BankContext());
    }

    [HttpGet("GetClient")]
    public Client GetClient(Guid Id)
    {
        var client = _clientService.GetClient(Id);
        return client;
    }

    [HttpPost("AddClient")]
    public void AddClient(Client client)
    {
        _clientService.AddClient(client);
    }

    [HttpDelete("DeleteClient")]
    public void DeleteClient(Guid id)
    {
        _clientService.RemoveClient(id);
    }
    [HttpPut("UpdateClient")]
    public void UpdateClient(Guid id, Client client)
    {
        _clientService.ChangeClient(id,client);
    }
}