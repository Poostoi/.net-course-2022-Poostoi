using Migration;
using Services;
using Services.ExceptionCraft;
using Services.Filters;
using Services.Storage;

namespace ServiceTests;

public class EmployeeServiceTests
{
    [Test]
    public void GetEmployees_EmployeeFilterAndEmployeeService_CountDictionaryMoreOne()
    {
        //arrange
        var filter = new EmployeeFilter()
        {
            Name = "Михаил",
            DateEnd = new DateTime(1992, 03, 15)
        };
        var employeeService = new EmployeeService(new BankContext());
        var employee = new TestDataGenerator().GeneratingEmployee();
        employee.Name = "Михаил";
        employee.DateBirth = new DateTime(1991, 02, 28);
        for (var i = 0; i < 50; i++)
            employeeService.AddEmployee(new TestDataGenerator().GeneratingEmployee());
        employeeService.AddEmployee(employee);
        //act
        var list = employeeService.GetEmployees(filter);
        //assert
        Assert.True(list.Count >= 1);
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
    public async Task ChangeEmployee_Employee_NotEqual()
    {
        //arrange
        var employeeOld = new TestDataGenerator().GeneratingEmployee();
        var employeeNew = new TestDataGenerator().GeneratingEmployee();
        var employeeService = new EmployeeService(new BankContext());

        //act
        employeeService.AddEmployee(employeeOld);
        var oldEmployeeDbInDB = await employeeService.GetEmployee(employeeOld.Id);
        var oldPassportId = oldEmployeeDbInDB.PassportId;
        employeeService.ChangeEmployee(employeeOld.Id, employeeNew);
        var newEmployee = await employeeService.GetEmployee(employeeOld.Id);
        //assert
        Assert.False(newEmployee.PassportId.Equals(oldPassportId));
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