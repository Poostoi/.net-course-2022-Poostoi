using Microsoft.EntityFrameworkCore;
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

    public async Task<Employee> GetEmployee(Guid employeeId)
    {
        var employeeDb = await _bankContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
        if (employeeDb == null) return null;
        return _mapperService.MapperFromEmployeeDbInEmployee.Map<Employee>(employeeDb);
    }

    public async Task AddEmployee(Employee employee)
    {
        await _bankContext.Employees.AddAsync(_mapperService.MapperFromEmployeeInEmployeeDb.Map<EmployeeDb>(employee));
        await _bankContext.SaveChangesAsync();
    }

    public async Task ChangeEmployee(Guid employeeId, Employee employee)
    {
        var employeeIdDatabase = await _bankContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
        employeeIdDatabase.Name = employee.Name;
        employeeIdDatabase.Surname = employee.Surname;
        employeeIdDatabase.DateBirth = employee.DateBirth;
        employeeIdDatabase.PassportId = employee.PassportId;
        employeeIdDatabase.Contract = employee.Contract;
        employeeIdDatabase.Salary = employee.Salary;
        employeeIdDatabase.Bonus = employee.Bonus;
        _bankContext.Update(employeeIdDatabase);
        await _bankContext.SaveChangesAsync();
    }

    public async Task RemoveEmployee(Guid employeeId)
    {
        var employeeIdDatabase = await _bankContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
        _bankContext.Employees.Remove(employeeIdDatabase);
        await _bankContext.SaveChangesAsync();
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