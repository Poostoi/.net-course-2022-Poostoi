using Migration;
using Services;
using Services.ExceptionCraft;
using Services.Filters;
using Services.Storage;

namespace ServiceTests;

public class EmployeeServiceTests
{
    [Test]
    public void AddEmployee_Employee_AgeLessException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var employeeStorage = new EmployeeStorage();
        var employeeService = new EmployeeService(employeeStorage);
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
        var employeeStorage = new EmployeeStorage();
        var employeeService = new EmployeeService(employeeStorage);
        var employee = dataGenerator.GeneratingEmployee();
        employee.PassportId = 0;
        employee.DateBirth = new DateTime(2000,4,13);
        //act assert 
        Assert.Throws<NotPassportDataException>(()=>employeeService.AddEmployee(employee));
    }

    [Test]
    public void GetEmployee_EmployeeFilterAndEmployeeStorage_CountDictionaryOne()
    {
        //arrange
        var filter = new EmployeeFilter()
        {
            Name = "Михаил",
            DateEnd = DateTime.Now
        };
        var employeeStorage = new EmployeeStorage();
        var employee = new TestDataGenerator().GeneratingEmployee();
        employee.Name = "Михаил";
        for (var i = 0; i < 50; i++)
            employeeStorage.Add(new TestDataGenerator().GeneratingEmployee());
        employeeStorage.Add(employee);
        var employeeService = new EmployeeService(employeeStorage);
        //act
        var dictionary = employeeService.GetEmployees(filter);
        var youngestClient = employeeStorage.Data.Max(c => c.DateBirth);
        var oldestClient = employeeStorage.Data.Min(c => c.DateBirth);
        var averageAge = employeeStorage.Data.Average(c => DateTime.Now.Year - c.DateBirth.Year);
        //assert
        Assert.True(dictionary.Count >= 1);
    }
    [Test]
    public void AddEmployeeDb_Employee_ContainEmployee()
    {
        //arrange
        var employeeDb = new TestDataGenerator().GeneratingEmployee();
        var clientService = new EmployeeService(new BankContext());

        //act
        clientService.AddEmployeeDb(employeeDb);
        //assert
        Assert.NotNull(clientService.GetEmployeeDb(employeeDb.Id));
    }
    
    
    [Test]
    public void ChangeEmployeeDb_Employee_NotEqual()
    {
        //arrange
        var employeeDbOld = new TestDataGenerator().GeneratingEmployee();
        var employeeDbNew = new TestDataGenerator().GeneratingEmployee();
        var employeeService = new EmployeeService(new BankContext());
        

        //act
        employeeService.AddEmployeeDb(employeeDbOld);
        var oldEmployeeDbInDB = employeeService.GetEmployeeDb(employeeDbOld.Id);
        var oldPassportId = oldEmployeeDbInDB.PassportId;
        employeeService.ChangeEmployeeDb(employeeDbOld.Id,employeeDbNew);
        //assert
        Assert.False(employeeService.GetEmployeeDb(employeeDbOld.Id).PassportId.Equals(oldPassportId));
    }
    [Test]
    public void DeleteEmployeeDb_Employee_NotEmployee()
    {
        //arrange
        var employeeDb = new TestDataGenerator().GeneratingEmployee();
        var employeeService = new EmployeeService(new BankContext());

        //act
        employeeService.AddEmployeeDb(employeeDb);
        employeeService.RemoveEmployeeDb(employeeDb.Id);
        //assert
        Assert.Null(employeeService.GetEmployeeDb(employeeDb.Id));
    }
}