using ExportTool;
using Migration;
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
        exportService.FromCsvFileInDatabase(path, fileName);
        var expectedClient = new ClientService(new BankContext()).GetClient(client.Id);
        //assert
        Assert.AreEqual(expectedClient,client);
    }
}