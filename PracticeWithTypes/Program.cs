using Models;
using Services;

var services = new BankService<Client>();
var employee = new Employee(){Surname = "Соколов",
    Name = "Сергей",
    PassportId = 12313123,
    DateBirth = DateTime.Now
};
var client = new Client()
{
    Surname = "Николаев",
    Name = "Борис",
    DateBirth = DateTime.Now,
    PassportId = 123123123
};

employee.Salary = services.CalculationBankOwnerSalary(3000,
    300, 5);
Console.WriteLine($"Зарплата одного из владельцев {employee.Surname}" +
                  $" {employee.Name}:" +
                  $" {employee.Salary}");
var employeeWhoWasClient = new Services.BankService<Client>().TransformationClientInEmployee(client);
var result = employeeWhoWasClient.Name == client.Name &&
             employeeWhoWasClient.Surname == client.Surname &&
             employeeWhoWasClient.DateBirth == client.DateBirth &&
             employeeWhoWasClient.PassportId == client.PassportId &&
             employeeWhoWasClient is Employee;
Console.WriteLine("Результат преобразования клиента в сотрудника: "+result);
static void UpdateContactEmployeeBad(Employee employee)
{
    var descriptionContract = $"Компания Dex.\n" +
                              $"С сотрудником: {employee.Surname} {employee.Name},\n" +
                              $"cерия паспорта: {employee.PassportId},\n" +
                              $"заключён контракт.";
    employee.Contract = descriptionContract;
}

static void UpdateCurrencyBad(ref Currency currency)
{
    currency.Code = 304;
    currency.Name = "Юани";
}

static string CreateContractGood(string surname, string name, int passportId) =>
    $"Компания Dex.\n" +
    $"С сотрудником: {surname} {name},\n" +
    $"cерия паспорта: {passportId},\n" +
    $"заключён контракт.";

static Currency UpdateCurrencyGood(int code, string name) => new() { Code = code, Name = name };