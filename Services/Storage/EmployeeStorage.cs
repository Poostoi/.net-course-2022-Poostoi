using Models;
using Services.ExceptionCraft;
using Services.Storage;

namespace Services;

public class EmployeeStorage: IStorage<Employee>
{
    public  List<Employee> Date { get; }

    public EmployeeStorage() => Date = new List<Employee>();
    public void Add(Employee employee) => Date.Add(employee);

    public void Update(Employee employee)
    {
        var employeeOld = Date.Find(e => e.PassportId == employee.PassportId);
        employeeOld = employee;
    }

    public void Delete(Employee employee) =>  throw new NotImplementedException(); 

    
}