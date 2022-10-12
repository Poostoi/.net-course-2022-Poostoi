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
        var bankContext = new BankContext();
        var convertService = new ConvertService();
        var rateUpdaterService = new RateUpdaterService();
        var testData = new TestDataGenerator();
        var token = new CancellationToken();
        var clients = new List<ClientDb>(20);
        for(int i = 0;i<10;i++)
        {
            clients.Add(convertService.ConvertClientInClientDb(new TestDataGenerator().GeneratingClient()));
            bankContext.Clients.Add(convertService.ConvertClientInClientDb(new TestDataGenerator().GeneratingClient()));
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