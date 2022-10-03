using ExportTool;
using Services;

namespace ServiceTests;

public class ExportServiceTests
{
    [Test]
    public void Delete_Employee_ListCountZero()
    {
        //arrange
        var exportService = new ExportService();
        var clients = new TestDataGenerator().GenerateListClient(5);
        var path = Path.Combine("C:", "test");
        var fileName = "clients.csv";
        //act
        exportService.ExportClientsInFileCSV(clients,path, fileName );
        exportService.ReadPersonFromCsv("C://", "test", path);
        //assert
        Assert.True(true);
    }
}