using Models;
using Services.ExceptionCraft;
using Services.Filters;

namespace Services;

public class EmployeeService
{
    private EmployeeStorage _employeeStorage;

    public EmployeeService(EmployeeStorage employeeStorage)
    {
        _employeeStorage = employeeStorage;
    }
    public void AddEmployee(Employee employee)
    {
        if (DateTime.Now.Year - employee.DateBirth.Year < 18)
            throw new AgeLessException("Возраст меньше 18.");
        if (employee.PassportId <= 0)
            throw new NotPassportDataException("У сотрудника нет пасспортных данных.");
        _employeeStorage.Add(employee);
    }
    public List<Employee> GetEmployees(EmployeeFilter employeeFilter)
    {
        var list = new List<Employee>();
        IEnumerable<Employee> request = null;
        if (employeeFilter.Name != null && employeeFilter.Name != "")
            request = _employeeStorage._employees.Where(c => 
                c.Name == employeeFilter.Name);
        if (employeeFilter.Surname != null && employeeFilter.Surname != "")
            request = _employeeStorage._employees.Where(c => 
                c.Surname == employeeFilter.Surname);
        if (employeeFilter.Salary != 0)
            request = _employeeStorage._employees.Where(c => 
                c.Salary == employeeFilter.Salary);
        if (employeeFilter.Contract != null && employeeFilter.Contract != "")
            request = _employeeStorage._employees.Where(c => 
                c.Salary == employeeFilter.Salary);
        if (employeeFilter.PassportId != 0)
            request = _employeeStorage._employees.Where(c => 
                c.PassportId == employeeFilter.PassportId);
        if (employeeFilter.DateStart != new DateTime())
            request = _employeeStorage._employees.Where(c => 
                c.DateBirth >= employeeFilter.DateStart);
        if (employeeFilter.DateEnd != new DateTime())
            request = _employeeStorage._employees.Where(c => 
                c.DateBirth <= employeeFilter.DateEnd);
        list = request.ToList();;

        return list;
    }
    
}