using Migration;
using Models;
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
        var clientDb = new TestDataGenerator().GeneratingClient();
        var clientService = new ClientService(new BankContext());

        //act
        clientService.AddClientDb(clientDb);
        //assert
        Assert.NotNull(clientService.GetClientDb(clientDb.Id));
    }
    [Test]
    public void AddAccountDb_Client_ContainAccountTwo()
    {
        //arrange
        var clientDb = new TestDataGenerator().GeneratingClient();
        var clientService = new ClientService(new BankContext());

        //act
        clientService.AddClientDb(clientDb);
        clientService.AddAccountDb(clientDb.Id);
        clientService.GetClientDb(clientDb.Id);
        //assert
        Assert.True(clientService.GetClientDb(clientDb.Id).Accounts.Count==2);
    }
    [Test]
    public void ChangeClientDb_Client_NotEqual()
    {
        //arrange
        var clientDbOld = new TestDataGenerator().GeneratingClient();
        var clientDbNew = new TestDataGenerator().GeneratingClient();
        var clientService = new ClientService(new BankContext());
        

        //act
        clientService.AddClientDb(clientDbOld);
        var oldClientInDB = clientService.GetClientDb(clientDbOld.Id);
        var oldPassportId = oldClientInDB.PassportId;
        clientService.ChangeClientDb(clientDbOld.Id,clientDbNew);
        //assert
        Assert.False(clientService.GetClientDb(clientDbOld.Id).PassportId.Equals(oldPassportId));
    }
    [Test]
    public void DeleteClientDb_Client_NotClient()
    {
        //arrange
        var clientDb = new TestDataGenerator().GeneratingClient();
        var clientService = new ClientService(new BankContext());

        //act
        clientService.AddClientDb(clientDb);
        clientService.RemoveClientDb(clientDb.Id);
        //assert
        Assert.Null(clientService.GetClientDb(clientDb.Id));
    }
}