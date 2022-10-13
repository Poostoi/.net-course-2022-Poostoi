using Migration;
using Services;

namespace ServiceTests;

public class RateUpdaterTests
{
    [Test]
    public void  UpdaterRate_BankContext_True()
    {
        //arrange
        var bankContext = new BankContext();
        var rateUpdaterService = new RateUpdaterService();
        var token = new CancellationTokenSource();
        

        //act assert
        var task = rateUpdaterService.UpdateRate(token, bankContext);
        task.Wait(20000);

        token.Cancel();
    }
}