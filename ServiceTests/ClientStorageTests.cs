using Models;
using Services;
using Services.Storage;

namespace ServiceTests;

public class ClientStorageTest
{
    [Test]
    public void Update_Client_ChangeClient()
    {
        //arrange
        var clientStorage = new ClientStorage();
        var testData = new TestDataGenerator();
        var client = testData.GeneratingClient();
        var clientUpdate = testData.GeneratingClient();
        //act
        clientStorage.Add(client);
        clientUpdate.PassportId = client.PassportId;
        clientStorage.Update(clientUpdate);
        var currentClient = clientStorage.Data.First(c => c.Key.Equals(client)).Key;
        //assert
        Assert.True(currentClient.Equals(clientUpdate));
    }
    [Test]
    public void UpdateAccount_Account_ChangeAccount()
    {
        //arrange
        var clientStorage = new ClientStorage();
        var testData = new TestDataGenerator();
        var client = testData.GeneratingClient();
        var account = testData.GeneratingAccount();
        var updateAccount = testData.GeneratingAccount();
        //act
        clientStorage.AddAccount(client,account);
        
        clientStorage.UpdateAccount(client, updateAccount);
        
        //assert
        Assert.True(clientStorage.Data[client].Equals(updateAccount));
    }
    
    [Test]
    public void Delete_Client_DictionaryCountZero()
    {
        //arrange
        var clientStorage = new ClientStorage();
        var testData = new TestDataGenerator();
        var client = testData.GeneratingClient();
        //act
        clientStorage.Add(client);
        clientStorage.Delete(client);
        //assert
        Assert.True(clientStorage.Data.Count ==0);
    }
    
}