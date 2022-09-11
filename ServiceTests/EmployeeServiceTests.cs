using Services;
using Services.ExceptionCraft;
using Services.Filters;

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
        var youngestClient = employeeStorage._employees.Max(c => c.DateBirth);
        var oldestClient = employeeStorage._employees.Min(c => c.DateBirth);
        var averageAge = employeeStorage._employees.Average(c => DateTime.Now.Year - c.DateBirth.Year);
        //assert
        Assert.True(dictionary.Count >= 1);
    }
}