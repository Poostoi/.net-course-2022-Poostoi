using Services;
using Services.Storage;

namespace ServiceTests;

public class EmployeeStorageTests
{
    [Test]
    public void Delete_Employee_ListCountZero()
    {
        //arrange
        var employeeStorage = new EmployeeStorage();
        var testData = new TestDataGenerator();
        var employee = testData.GeneratingEmployee();
        
        //act
        employeeStorage.Add(employee);
        employeeStorage.Delete(employee);
        
        //assert
        Assert.True(employeeStorage.Data.Count==0);
    }
    [Test]
    public void Update_Employee_ChangeEmployee()
    {
        //arrange
        var employeeStorage = new EmployeeStorage();
        var testData = new TestDataGenerator();
        var employee = testData.GeneratingEmployee();
        var employeeUpdate = testData.GeneratingEmployee();
        //act
        employeeStorage.Add(employee);
        employeeUpdate.PassportId = employee.PassportId;
        employeeStorage.Update(employeeUpdate);
        //assert
        Assert.True(employeeStorage.Data[0].Equals(employeeUpdate));
    }
}