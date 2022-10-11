using Migration;
using Models;
using ModelsDb;
using Services.Filters;

namespace Services;

public class RateUpdaterService
{
    public void UpdateRate(CancellationToken token, BankContext bankContext)
    {
        var clientService = new ClientService(bankContext);
        var clients = clientService.GetClientsLimit(new ClientFilter(), 10);
        var accountsDb = bankContext.Accounts.Take(10); 
        
        while (token.IsCancellationRequested)
        {
            foreach (var account in accountsDb)
            {
                var accountUpdate = new Account()
                {
                    Amount = account.Amount + 5,
                    Currency = new Currency()
                    {
                        Code = account.CurrencyDb.Code,
                        Id = account.CurrencyDb.Id,
                        Name = account.CurrencyDb.Name
                    },
                    Id = account.Id
                };
                clientService.UpdateAccount(accountUpdate);                
            }
        }
    }
}