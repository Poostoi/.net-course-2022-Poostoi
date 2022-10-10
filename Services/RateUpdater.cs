using Migration;
using Models;
using ModelsDb;
using Services.Filters;

namespace Services;

public class RateUpdater
{
    public void UpdateRate(CancellationToken token)
    {
        var bankContext = new BankContext();
        var clientService = new ClientService(bankContext);
        var clients = clientService.GetClientsLimit(new ClientFilter(), 10);
        List<ClientDb> clientDbs = new List<ClientDb>();
        foreach (var client in clients)
        {
            clientDbs.Add(bankContext.Clients.FirstOrDefault(c => c.Id == client.Id));
        }

        var account = clientDbs[0].AccountsDbs[0];
        while (token.IsCancellationRequested)
        {
            account.Amount += 5;
            bankContext.Update(account);
            bankContext.SaveChanges();
        }
    }
}