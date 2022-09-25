using Migration;
using Models;
using Services.ExceptionCraft;
using Services.Filters;
using Services.Storage;

namespace Services;

public class EmployeeService
{
    private EmployeeStorage _employeeStorage;
    private BankContext _bankContext;

    public EmployeeService(EmployeeStorage employeeStorage)
    {
        _employeeStorage = employeeStorage;
    }

    public EmployeeService(BankContext bankContext)
    {
        _bankContext = bankContext;
    }

    public void AddEmployee(EmployeeDb employeeDb)
    {
        if (DateTime.Now.Year - employeeDb.DateBirth.Year < 18)
            throw new AgeLessException("Возраст меньше 18.");
        if (employeeDb.PassportId <= 0)
            throw new NotPassportDataException("У сотрудника нет пасспортных данных.");
        _employeeStorage.Add(employeeDb);
    }

    public List<EmployeeDb> GetEmployees(EmployeeFilter employeeFilter)
    {
        var request = _employeeStorage.Data.Select(c => c);

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

        return request.ToList();
    }

    public EmployeeDb GetEmployeeDb(Guid employeeId)
    {
        return _bankContext.Employees.FirstOrDefault(e => e.Id == employeeId);
    }

    public void AddEmployeeDb(EmployeeDb employeeDb)
    {
        _bankContext.Employees.Add(employeeDb);
        _bankContext.SaveChanges();
    }

    public void ChangeEmployeeDb(Guid employeeId, EmployeeDb employeeDb)
    {
        var employeeIdDatabase = GetEmployeeDb(employeeId);
        employeeIdDatabase.Name = employeeDb.Name;
        employeeIdDatabase.Surname = employeeDb.Surname;
        employeeIdDatabase.DateBirth = employeeDb.DateBirth;
        employeeIdDatabase.PassportId = employeeDb.PassportId;
        employeeIdDatabase.Contract = employeeDb.Contract;
        employeeIdDatabase.Salary = employeeDb.Salary;
        employeeIdDatabase.Bonus = employeeDb.Bonus;
        _bankContext.SaveChanges();
    }

    public void RemoveEmployeeDb(Guid employeeDbId)
    {
        _bankContext.Employees.Remove(GetEmployeeDb(employeeDbId));
        _bankContext.SaveChanges();
    }

    public List<EmployeeDb> GetEmployeeDbs(EmployeeFilter employeeFilter)
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

        return request.ToList();
    }
}