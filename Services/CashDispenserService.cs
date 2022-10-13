using Migration;
using ModelsDb;

namespace Services;

public class CashDispenserService
{
    public Task CashingOutInAccount(CancellationTokenSource token, BankContext bankContext,AccountDb accountDb)
    {
        var clientService = new ClientService(bankContext);
        var convertService = new ConvertService();
        return Task.Run(() =>
        {
            while (!token.IsCancellationRequested)
            {
                if (accountDb.Amount >= 5)
                {
                    accountDb.Amount -= 5;
                    clientService.UpdateAccount(convertService.ConvertAccountDbInAccount(accountDb));
                    Task.Delay(5000).Wait();
                }
                else throw new ArgumentException("Недостаточно средств на счету.");
            }
        });
    }
}