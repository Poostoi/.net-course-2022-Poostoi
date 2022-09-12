using Models;
using Services.ExceptionCraft;

namespace Services;

public class EmployeeStorage
{
    public readonly List<Employee> _employees = new List<Employee>();

    public void Add(Employee employee)
    {
        _employees.Add(employee);
    }
    public void Update()
    {
        
    }
    public void Remove()
    {
        
    }
}