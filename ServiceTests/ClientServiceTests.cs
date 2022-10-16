using Bogus;
using Migration;
using Models;
using ModelsDb;
using Services;
using Services.ExceptionCraft;
using Services.Filters;
using Services.Storage;

namespace ServiceTests;

public class ClientServiceTests
{
    [Test]
    public void AddClient_Client_AgeLessException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var clientService = new ClientService(new ClientStorage());
        var client = dataGenerator.GeneratingClient();
        client.DateBirth = DateTime.Now;

        //assert act
        Assert.Throws<AgeLessException>(() => clientService.AddAccount(client));
    }

    [Test]
    public void AddClient_Client_NotPassportDataException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var clientService = new ClientService(new ClientStorage());
        var client = dataGenerator.GeneratingClient();
        client.DateBirth = new DateTime(1970, 12, 27);
        client.PassportId = 0;
        //assert act
        Assert.Throws<NotPassportDataException>(() => clientService.AddAccount(client));
    }
    [Test]
    public void AddClient_Client_ContainClient()
    {
        //arrange
        var client = new TestDataGenerator().GeneratingClient();
        var clientNew = new TestDataGenerator().GeneratingClient();
        var clientService = new ClientService(new BankContext());

        //act
        clientService.AddClient(client);
        clientService.ChangeClient(client.Id, clientNew);
        //assert
        Assert.NotNull(clientService.GetClient(client.Id));
    }
    
    [Test]
    public async Task ChangeClient_Client_NotEqual()
    {
        //arrange
        var clientDbOld = new TestDataGenerator().GeneratingClient();
        var clientDbNew = new TestDataGenerator().GeneratingClient();
        var clientService = new ClientService(new BankContext());
        

        //act
        clientService.AddClient(clientDbOld);
        var oldClientInDB = await clientService.GetClient(clientDbOld.Id);
        var oldPassportId = oldClientInDB.PassportId;
        clientService.ChangeClient(clientDbOld.Id,clientDbNew);
        var newClient = await clientService.GetClient(clientDbOld.Id);
        
        //assert
        Assert.False(newClient.PassportId.Equals(oldPassportId));
    }
    [Test]
    public void DeleteClient_Client_NotClient()
    {
        //arrange
        var client = new TestDataGenerator().GeneratingClient();
        var clientService = new ClientService(new BankContext());

        //act
        clientService.AddClient(client);
        clientService.RemoveClient(client.Id);
        //assert
        Assert.Null(clientService.GetClient(client.Id));
    }
    [Test]
    public void GetClient_ClientFilterAndClientService_CountDictionaryOne()
    {
        //arrange
        var filter = new ClientFilter()
        {
            Name = "Михаил",
            DateEnd = new DateTime(1992, 03, 15)
        };
        var clientService = new ClientService(new BankContext());
        var client = new TestDataGenerator().GeneratingClient();
        client.Name = "Михаил";
        client.DateBirth = new DateTime(1991, 02, 28);
        for (var i = 0; i < 50; i++)
            clientService.AddClient(new TestDataGenerator().GeneratingClient());
        clientService.AddClient(client);
        //act
        var list = clientService.GetClients(filter);
        //assert
        Assert.True(list.Count >= 1);
    }
    
}