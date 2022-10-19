using Microsoft.AspNetCore.Mvc;
using Migration;
using Models;
using Services;

namespace BankApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeControllers : ControllerBase
{
    public EmployeeService _employeeService;

    public EmployeeControllers()
    {
        _employeeService = new EmployeeService(new BankContext());
    }

    [HttpGet("GetEmployee")]
    public Employee GetEmployee(Guid Id)
    {
        var employee = _employeeService.GetEmployee(Id);
        return employee;
    }

    [HttpPost("AddEmployee")]
    public void AddEmployee(Employee employee)
    {
        _employeeService.AddEmployee(employee);
    }

    [HttpDelete("DeleteEmployee")]
    public void DeleteEmployee(Guid id)
    {
        _employeeService.RemoveEmployee(id);
    }

    [HttpPut("UpdateEmployee")]
    public void UpdateEmployee(Guid id, Employee employee)
    {
        _employeeService.ChangeEmployee(id, employee);
    }
}