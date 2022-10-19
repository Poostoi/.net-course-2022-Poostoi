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
    public async Task<Employee> GetEmployee(Guid Id)
    {
        return await _employeeService.GetEmployee(Id);
    }

    [HttpPost("AddEmployee")]
    public async Task AddEmployee(Employee employee)
    {
        await _employeeService.AddEmployee(employee);
    }

    [HttpDelete("DeleteEmployee")]
    public async Task DeleteEmployee(Guid id)
    {
        await _employeeService.RemoveEmployee(id);
    }

    [HttpPut("UpdateEmployee")]
    public async Task UpdateEmployee(Guid id, Employee employee)
    {
        await _employeeService.ChangeEmployee(id, employee);
    }
}