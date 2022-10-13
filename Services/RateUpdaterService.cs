using Migration;

namespace Services;

public class RateUpdaterService
{
    public Task UpdateRate(CancellationTokenSource token, BankContext bankContext)
    {
        var clientService = new ClientService(bankContext);
        var convertService = new ConvertService();
        return Task.Run(() =>
        {
            while (!token.IsCancellationRequested)
            {
                var accountsDb = bankContext.Accounts.Take(10).ToList();

                foreach (var accountDb in accountsDb)
                {
                    accountDb.Amount += 5;
                    clientService.UpdateAccount(convertService.ConvertAccountDbInAccount(accountDb));
                }

                Task.Delay(5000).Wait();
            }
        });
    }
}