using Models;
using Services.Exception;

namespace Services;

public class EmployeeService
{
    private List<Employee> _employees = new List<Employee>();

    public void AddEmployee(Employee employee)
    {
        if (_employees.Contains(employee))
            throw new ArgumentException("Такой сотрудник уже существует");
        if (DateTime.Now.Year - employee.DateBirth.Year < 18)
            throw new AgeLessException("Возраст меньше 18.");
        if (employee.PassportId <= 0)
            throw new NotPassportDataException("У сотрудника нет пасспортных данных.");
        _employees.Add(employee);
    }
}