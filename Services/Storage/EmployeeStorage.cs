using Models;

namespace Services.Storage;

public class EmployeeStorage: IStorage<Employee>
{
    public  List<Employee> Data { get; }

    public EmployeeStorage() => Data = new List<Employee>();
    public void Add(Employee employee) => Data.Add(employee);

    public void Update(Employee employee)
    {
        var employeeOld = Data.Find(e => e.PassportId == employee.PassportId);
        if (employeeOld == null) return;
        employeeOld.Contract = employee.Contract;
        employeeOld.Name = employee.Name;
        employeeOld.Surname = employee.Surname;
        employeeOld.DateBirth = employee.DateBirth;
        employeeOld.Salary = employee.Salary;
    }

    public void Delete(Employee employee)
    {
        if (!Data.Contains(employee)) return;
        Data.Remove(employee);
    }
}