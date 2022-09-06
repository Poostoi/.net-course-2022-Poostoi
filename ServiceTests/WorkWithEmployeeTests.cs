using Services;
using Services.ExceptionCraft;

namespace ServiceTests;

public class WorkWithEmployeeTests
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
}