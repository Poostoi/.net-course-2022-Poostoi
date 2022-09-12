namespace Services.Filters;

public class EmployeeFilter
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public int PassportId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public int Salary { get; set; }
    public string Contract { get; set; }
}