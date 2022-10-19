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
    public async Task<Client> GetClient(Guid Id)
    {
        return await _clientService.GetClient(Id);
    }

    [HttpPost("AddClient")]
    public async Task AddClient(Client client)
    {
        await _clientService.AddClient(client);
    }

    [HttpDelete("DeleteClient")]
    public async Task DeleteClient(Guid id)
    {
        await _clientService.RemoveClient(id);
    }
    [HttpPut("UpdateClient")]
    public async Task UpdateClient(Guid id, Client client)
    {
        await _clientService.ChangeClient(id,client);
    }
}