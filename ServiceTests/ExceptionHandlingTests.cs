using Services;
using Services.Exception;

namespace ServiceTests;

public class ExceptionHandlingTests
{
    [Test]
    public void AddEmployee_Employee_AgeLessException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var employeeService = new EmployeeService();
        var employee = dataGenerator.GeneratingEmployee();
        employee.DateBirth = DateTime.Now;
        //act assert 
        Assert.Throws<AgeLessException>(()=>employeeService.AddEmployee(employee));
    }
    [Test]
    public void AddEmployee_Employee_NotPassportDataException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var employeeService = new EmployeeService();
        var employee = dataGenerator.GeneratingEmployee();
        employee.PassportId = 0;
        employee.DateBirth = new DateTime(2000,4,13);
        //act assert 
        Assert.Throws<NotPassportDataException>(()=>employeeService.AddEmployee(employee));
    }
    [Test]
    public void AddClient_Client_ArgumentException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var clientService = new ClientService();
        var client = dataGenerator.GeneratingClient();
        var account = dataGenerator.GeneratingAccount();
        client.DateBirth = new DateTime(1970, 12, 27);
        
        //act
        clientService.AddAccount(client, account);
        //assert 
        Assert.Throws<ArgumentException>(()=>clientService.AddAccount(client,account));
    }
    [Test]
    public void AddClient_Client_AgeLessException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var clientService = new ClientService();
        var client = dataGenerator.GeneratingClient();
        var account = dataGenerator.GeneratingAccount();
        client.DateBirth = DateTime.Now;
        
        //assert act
        Assert.Throws<AgeLessException>(()=>clientService.AddAccount(client,account));
    }
    [Test]
    public void AddClient_Client_NotPassportDataException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var clientService = new ClientService();
        var client = dataGenerator.GeneratingClient();
        var account = dataGenerator.GeneratingAccount();
        client.DateBirth = new DateTime(1970, 12, 27);
        client.PassportId = 0;
        //assert act
        Assert.Throws<NotPassportDataException>(()=>clientService.AddAccount(client,account));
    }
}