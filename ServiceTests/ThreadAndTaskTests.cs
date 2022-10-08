using Services;
using System.Threading;
using ExportTool;
using Migration;
using Models;
using Services.Filters;

namespace ServiceTests;

public class ThreadAndTaskTests
{
    private object _key = new();
    private Account _account = new TestDataGenerator().GeneratingAccount();

    [Test]
    public void ParallelAccrual_Client_Equal2000()
    {
        //arrange
        _account.Amount = 0;
        Thread firstThread = new(Accrual);
        firstThread.Name = "Первый";
        Thread secondThread = new(Accrual);
        secondThread.Name = "Второй";

        //act
        firstThread.Start(10);
        secondThread.Start(10);

        //assert
        Thread.Sleep(10000);
        Assert.AreEqual(_account.Amount, 2000);
    }


    private void Accrual(object? o)
    {
        var count = (int)o!;
        lock (_key)
        {
            for (int i = 0; i < count; i++)
            {
                _account.Amount += 100;
                Console.WriteLine(
                    $"{i}.{Thread.CurrentThread.Name} поток, значение счёта увеличилось на 100, сейчас: {_account.Amount}");
            }
        }
    }

    [Test]
    public void ExportFromDbAndImportInDb_PathOnFiles_StringMore1000()
    {
        //arrange
        var clientService = new ClientService(new BankContext());
        var countUpToUpdate = clientService.GetClients(new ClientFilter()).Count;
        Thread firstThread = new(ImportDb);
        firstThread.Name = "Первый";
        Thread secondThread = new(ExportDb);
        secondThread.Name = "Второй";
        var path = Path.Combine("C:", "test");
        var fileName = "clients.csv";
        //act
        firstThread.Start();
        secondThread.Start();
       
        Thread.Sleep(10000);
        List<Client> clientsFromDb = clientService.GetClients(new ClientFilter());
        var clients = new ExportService().ReadPersonFromCsv(path, fileName);
        //assert
        Assert.True(clients.Count  >= countUpToUpdate );
    }

    private void ImportDb()
    {
        var clients = new TestDataGenerator().GenerateListClient(1000);
        var clientService = new ClientService(new BankContext());
        foreach (var c in clients)
        {
            clientService.AddClient(c);
            
        }
    }

    private void ExportDb()
    {
        var clientService = new ClientService(new BankContext());
        List<Client> clients = clientService.GetClients(new ClientFilter());
        var path = Path.Combine("C:", "test");
        var fileName = "clients.csv";
        new ExportService().ExportClientsInFileCSV(clients, path, fileName);
    }
}