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
    public void AddClientDb_Client_ContainClient()
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
    public void ChangeClientDb_Client_NotEqual()
    {
        //arrange
        var clientDbOld = new TestDataGenerator().GeneratingClient();
        var clientDbNew = new TestDataGenerator().GeneratingClient();
        var clientService = new ClientService(new BankContext());
        

        //act
        clientService.AddClient(clientDbOld);
        var oldClientInDB = clientService.GetClient(clientDbOld.Id);
        var oldPassportId = oldClientInDB.PassportId;
        clientService.ChangeClient(clientDbOld.Id,clientDbNew);
        //assert
        Assert.False(clientService.GetClient(clientDbOld.Id).PassportId.Equals(oldPassportId));
    }
    [Test]
    public void DeleteClientDb_Client_NotClient()
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
    public void GetClients_Client_NotClient()
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
}