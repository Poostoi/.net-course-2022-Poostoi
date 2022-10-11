using Migration;
using Models;
using ModelsDb;
using Services;

namespace ServiceTests;

public class RateUpdaterTests
{
    [Test]
    public void UpdaterRate_BankContext_True()
    {
        //arrange
        var mapperService = new MapperService();
        var bankContext = new BankContext();
        var rateUpdaterService = new RateUpdaterService();
        var token = new CancellationToken();
        var clients = new TestDataGenerator().GenerateListClient(20);
        foreach (var client in clients)
        {
            client
        }
        var client = mapperService.MapperFromClientInClientDb.Map<ClientDb>
            (new TestDataGenerator().GeneratingClient());
        client.AccountsDbs = new List<AccountDb> { mapperService.MapperFromAccountInAccountDb.Map<AccountDb>(new TestDataGenerator().GeneratingAccount()) };

        for (int i = 0; i < 20; i++)
        {
            bankContext.Clients.Add(client);
            bankContext.SaveChanges();
        }
        //act

        Task.Factory.StartNew(() =>
        {
            rateUpdaterService.UpdateRate(token, bankContext);
            Task.Delay(10000).Wait();
        });


        //assert
    }
}