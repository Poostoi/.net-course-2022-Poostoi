using Migration;
using Services;
using Services.ExceptionCraft;
using Services.Filters;
using Services.Storage;

namespace ServiceTests;

public class EmployeeServiceTests
{

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
    public void AddEmployee_Employee_ContainEmployee()
    {
        //arrange
        var employee = new TestDataGenerator().GeneratingEmployee();
        var employeeService = new EmployeeService(new BankContext());

        //act
        employeeService.AddEmployee(employee);
        //assert
        Assert.NotNull(employeeService.GetEmployee(employee.Id));
    }
    
    
    [Test]
    public void ChangeEmployee_Employee_NotEqual()
    {
        //arrange
        var employeeOld = new TestDataGenerator().GeneratingEmployee();
        var employeeNew = new TestDataGenerator().GeneratingEmployee();
        var employeeService = new EmployeeService(new BankContext());
        

        //act
        employeeService.AddEmployee(employeeOld);
        var oldEmployeeDbInDB = employeeService.GetEmployee(employeeOld.Id);
        var oldPassportId = oldEmployeeDbInDB.PassportId;
        employeeService.ChangeEmployee(employeeOld.Id,employeeNew);
        //assert
        Assert.False(employeeService.GetEmployee(employeeOld.Id).PassportId.Equals(oldPassportId));
    }
    [Test]
    public void DeleteEmployee_Employee_NotEmployee()
    {
        //arrange
        var employee = new TestDataGenerator().GeneratingEmployee();
        var employeeService = new EmployeeService(new BankContext());

        //act
        employeeService.AddEmployee(employee);
        employeeService.RemoveEmployee(employee.Id);
        //assert
        Assert.Null(employeeService.GetEmployee(employee.Id));
    }
}