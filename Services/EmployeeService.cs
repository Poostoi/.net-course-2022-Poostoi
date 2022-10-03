using Migration;
using Models;
using ModelsDb;
using Services.ExceptionCraft;
using Services.Filters;
using Services.Storage;

namespace Services;

public class EmployeeService
{
    private EmployeeStorage _employeeStorage;
    private BankContext _bankContext;
    private MapperService _mapperService;


    public EmployeeService(EmployeeStorage employeeStorage)
    {
        _employeeStorage = employeeStorage;
    }

    public EmployeeService(BankContext bankContext)
    {
        _bankContext = bankContext;
        _mapperService = new MapperService();
    }

    public Employee GetEmployee(Guid employeeId)
    {
        var employeeDb = _bankContext.Employees.FirstOrDefault(e => e.Id == employeeId);
        if (employeeDb == null) return null;
        return _mapperService.MapperFromEmployeeDbInEmployee.Map<Employee>(employeeDb);
    }

    public void AddEmployee(Employee employee)
    {
        _bankContext.Employees.Add(_mapperService.MapperFromEmployeeInEmployeeDb.Map<EmployeeDb>(employee));
        _bankContext.SaveChanges();
    }

    public void ChangeEmployee(Guid employeeId, Employee employee)
    {
        var employeeIdDatabase = _bankContext.Employees.FirstOrDefault(e => e.Id == employeeId);
        employeeIdDatabase.Name = employee.Name;
        employeeIdDatabase.Surname = employee.Surname;
        employeeIdDatabase.DateBirth = employee.DateBirth;
        employeeIdDatabase.PassportId = employee.PassportId;
        employeeIdDatabase.Contract = employee.Contract;
        employeeIdDatabase.Salary = employee.Salary;
        employeeIdDatabase.Bonus = employee.Bonus;
        _bankContext.Update(employeeIdDatabase);
        _bankContext.SaveChanges();
    }

    public void RemoveEmployee(Guid employeeId)
    {
        var employeeIdDatabase = _bankContext.Employees.FirstOrDefault(e => e.Id == employeeId);
        _bankContext.Employees.Remove(employeeIdDatabase);
        _bankContext.SaveChanges();
    }

    public List<Employee> GetEmployees(EmployeeFilter employeeFilter)
    {
        var request = _bankContext.Employees.Select(c => c);

        if (employeeFilter.Name != null && employeeFilter.Name != "")
            request = request.Where(c =>
                c.Name == employeeFilter.Name);
        if (employeeFilter.Surname != null && employeeFilter.Surname != "")
            request = request.Where(c =>
                c.Surname == employeeFilter.Surname);
        if (employeeFilter.Salary != 0)
            request = request.Where(c =>
                c.Salary == employeeFilter.Salary);
        if (employeeFilter.Contract != null && employeeFilter.Contract != "")
            request = request.Where(c =>
                c.Salary == employeeFilter.Salary);
        if (employeeFilter.PassportId != 0)
            request = request.Where(c =>
                c.PassportId == employeeFilter.PassportId);
        if (employeeFilter.DateStart != new DateTime())
            request = request.Where(c =>
                c.DateBirth >= employeeFilter.DateStart);
        if (employeeFilter.DateEnd != new DateTime())
            request = request.Where(c =>
                c.DateBirth <= employeeFilter.DateEnd);
        var employeeDbs = request.ToList();
        return employeeDbs.Select(employeeDb =>
                _mapperService.MapperFromEmployeeDbInEmployee.Map<Employee>(employeeDb))
            .ToList();
    }
}