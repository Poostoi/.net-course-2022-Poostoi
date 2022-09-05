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
        Assert.Throws<Services.Exception.AgeLessException>(()=>employeeService.AddEmployee(employee));
    }
    public void AddEmployee_Employee_ClientNotPassportDataException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var employeeService = new EmployeeService();
        var employee = dataGenerator.GeneratingEmployee();
        employee.PassportId = 0;
        //act assert 
        Assert.Throws<ClientNotPassportDataException>(()=>employeeService.AddEmployee(employee));
    }
}