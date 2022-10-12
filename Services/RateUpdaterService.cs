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
                        Code = account.Currency.Code,
                        Id = account.Currency.Id,
                        Name = account.Currency.Name
                    },
                    Id = account.Id
                };
                clientService.UpdateAccount(accountUpdate);                
            }
        }
    }
}