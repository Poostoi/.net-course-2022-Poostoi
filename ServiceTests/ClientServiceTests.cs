using Services;
using Services.ExceptionCraft;
using Services.Filters;

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
    public void GetClients_ClientFilterAndClientStorage_CountDictionaryOne()
    {
        //arrange
        var filter = new ClientFilter()
        {
            Name = "Михаил",
        };
        var clientStorage = new ClientStorage();
        var client = new TestDataGenerator().GeneratingClient();
        client.Name = "Михаил";
        for (var i = 0; i < 50; i++)
            clientStorage.Add(new TestDataGenerator().GeneratingClient());
        clientStorage.Add(client);
        var clientService = new ClientService(clientStorage);
        //act
        var dictionary = clientService.GetClients(filter);
        var youngestClient = clientStorage._clients.Max(c => c.Key.DateBirth);
        var oldestClient = clientStorage._clients.Min(c => c.Key.DateBirth);
        var averageAge = clientStorage._clients.Average(c =>DateTime.Now.Year - c.Key.DateBirth.Year);
        //assert
        Assert.True(dictionary.Count >=1);
    }

    

   
}