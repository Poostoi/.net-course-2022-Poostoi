using ExportTool;
using Migration;
using Models;
using Services;

namespace ServiceTests;

public class ExportServiceTests
{
    [Test]
    public void ExportClientsInFileCSV_ListClient_ListClientFromFile()
    {
        //arrange
        var exportService = new ExportService();
        var clients = new TestDataGenerator().GenerateListClient(5);
        var path = Path.Combine("C:", "test");
        var fileName = "clients.csv";
        //act
        exportService.ExportClientsInFileCSV(clients, path, fileName);
        var tests = exportService.ReadPersonFromCsv(path, fileName);
        //assert
        Assert.NotNull(tests);
    }

    [Test]
    public void FromCsvFileInDatabase_ListClient_ClientEqualsClientInDb()
    {
        //arrange
        var exportService = new ExportService();
        var clients = new TestDataGenerator().GenerateListClient(5);
        var client = new TestDataGenerator().GeneratingClient();
        clients.Add(client);
        var path = Path.Combine("C:", "test");
        var fileName = "clients.csv";
        //act
        exportService.ExportClientsInFileCSV(clients, path, fileName);

        var clientInFile = exportService.ReadPersonFromCsv(path, fileName);
        if (clientInFile == null) return;
        var clientService = new ClientService(new BankContext());
        foreach (var c in clientInFile)
        {
            clientService.AddClient((Client)c);
        }
        var expectedClient = new ClientService(new BankContext()).GetClient(client.Id);
        var result  = expectedClient.Name == client.Name&&
                      expectedClient.Surname == client.Surname&&
                      expectedClient.NumberPhone == client.NumberPhone&&
                      expectedClient.Bonus == client.Bonus&&
                      expectedClient.DateBirth == client.DateBirth&&
                      expectedClient.Id == client.Id;
        //assert
        Assert.True(result);
    }
}