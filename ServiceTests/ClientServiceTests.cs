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
            Surname = "Николаев",
            DateStart = new DateTime(1999, 11, 1),
            DateEnd = DateTime.Now
        };
        var clientStorage = new ClientStorage();
        var client = new TestDataGenerator().GeneratingClient();
        client.Name = "Михаил";
        client.Surname = "Николаев";
        client.DateBirth = new DateTime(2000, 11, 2);

        //act
        for (var i = 0; i < 50; i++)
            clientStorage.Add(new TestDataGenerator().GeneratingClient());

        clientStorage.Add(client);
        var clientService = new ClientService(clientStorage);
        var dictionary = clientService.GetClients(filter);

        //assert
        var averageAge = clientStorage.Data.Average(c => DateTime.Now.Year - c.Key.DateBirth.Year);
        Assert.True(dictionary.Count == 1);
    }

    [Test]
    public void GetClients_OldestClient_EqualTrue()
    {
        //arrange
        var clientStorage = new ClientStorage();
        var oldestClient = new TestDataGenerator().GeneratingClient();
        oldestClient.DateBirth = new DateTime(1899, 11, 2);

        //act
        for (var i = 0; i < 50; i++)
            clientStorage.Add(new TestDataGenerator().GeneratingClient());
        clientStorage.Add(oldestClient);

        //assert
        Assert.That(clientStorage.Data.Min(c => c.Key.DateBirth), Is.EqualTo(oldestClient.DateBirth));
    }

    [Test]
    public void GetClients_YoungestClient_EqualTrue()
    {
        //arrange
        var clientStorage = new ClientStorage();
        var youngestClient = new TestDataGenerator().GeneratingClient();
        youngestClient.DateBirth = new DateTime(2001, 11, 2);

        //act
        for (var i = 0; i < 50; i++)
            clientStorage.Add(new TestDataGenerator().GeneratingClient());
        clientStorage.Add(youngestClient);

        //assert
        Assert.That(clientStorage.Data.Max(c => c.Key.DateBirth), Is.EqualTo(youngestClient.DateBirth));
    }
}