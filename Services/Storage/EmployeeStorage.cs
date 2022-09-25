using Models;

namespace Services.Storage;

public class EmployeeStorage: IStorage<EmployeeDb>
{
    public  List<EmployeeDb> Data { get; }

    public EmployeeStorage() => Data = new List<EmployeeDb>();
    public void Add(EmployeeDb employeeDb) => Data.Add(employeeDb);

    public void Update(EmployeeDb employeeDb)
    {
        var employeeOld = Data.Find(e => e.PassportId == employeeDb.PassportId);
        if (employeeOld == null) return;
        employeeOld.Contract = employeeDb.Contract;
        employeeOld.Name = employeeDb.Name;
        employeeOld.Surname = employeeDb.Surname;
        employeeOld.DateBirth = employeeDb.DateBirth;
        employeeOld.Salary = employeeDb.Salary;
    }

    public void Delete(EmployeeDb employeeDb)
    {
        if (!Data.Contains(employeeDb)) return;
        Data.Remove(employeeDb);
    }
}