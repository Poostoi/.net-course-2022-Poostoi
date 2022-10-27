using Microsoft.AspNetCore.Mvc;
using Migration;
using Models;
using Services;
using Services.ExceptionCraft;

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
    public async Task<ActionResult<Client>> GetClient(Guid Id)
    {
        var client = await _clientService.GetClient(Id);
        if (client != null)
            return client;
        return new BadRequestObjectResult(new DoesNotExistException("Такой пользователь не существует!"));
    }

    [HttpPost("AddClient")]
    public async Task<ActionResult> AddClient(Client client)
    {
        if ( await _clientService.GetClient(client.Id) != null)
        {
            return new BadRequestObjectResult(new AlreadyExistsException("Такой пользователь уже существует!"));
        }
        await _clientService.AddClient(client);
        return Ok();
    }

    [HttpDelete("DeleteClient")]
    public async Task<ActionResult> DeleteClient(Guid id)
    {
        if (await _clientService.GetClient(id) == null)
        {
            return new BadRequestObjectResult(new DoesNotExistException("Такой пользователь не существует!"));
        }
        await _clientService.RemoveClient(id);
        return Ok();
    }
    [HttpPut("UpdateClient")]
    public async Task<ActionResult> UpdateClient(Guid id, Client client)
    {
        if (await _clientService.GetClient(id) == null)
        {
            return new BadRequestObjectResult(new DoesNotExistException("Такой пользователь не существует!"));
        }
        await _clientService.ChangeClient(id,client);
        return Ok();
    }
}