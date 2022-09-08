using Services;
using Services.ExceptionCraft;

namespace ServiceTests;

public class ClientServiceTests
{
    [Test]
    public void AddClient_Client_IsKeyExistInDictionaryException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var clientService = new ClientService(new ClientStorage());
        var client = dataGenerator.GeneratingClient();
        var account = dataGenerator.GeneratingAccount();
        client.DateBirth = new DateTime(1970, 12, 27);
        
        //act
        clientService.AddAccount(client, account);
        //assert 
        Assert.Throws<IsKeyExistInDictionaryException>(()=>clientService.AddAccount(client,account));
    }
    [Test]
    public void AddClient_Client_AgeLessException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var clientService = new ClientService(new ClientStorage());
        var client = dataGenerator.GeneratingClient();
        var account = dataGenerator.GeneratingAccount();
        client.DateBirth = DateTime.Now;
        
        //assert act
        Assert.Throws<AgeLessException>(()=>clientService.AddAccount(client,account));
    }
    [Test]
    public void AddClient_Client_NotPassportDataException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var clientService = new ClientService(new ClientStorage());
        var client = dataGenerator.GeneratingClient();
        var account = dataGenerator.GeneratingAccount();
        client.DateBirth = new DateTime(1970, 12, 27);
        client.PassportId = 0;
        //assert act
        Assert.Throws<NotPassportDataException>(()=>clientService.AddAccount(client,account));
    }
}