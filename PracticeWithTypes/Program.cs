using Models;
using Services;

var services = new BankService<ClientDb>();
var employee = new EmployeeDb(){Surname = "Соколов",
    Name = "Сергей",
    PassportId = 12313123,
    DateBirth = DateTime.Now
};
var client = new ClientDb()
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
var employeeWhoWasClient = new Services.BankService<ClientDb>().TransformationClientInEmployee(client);
var result = employeeWhoWasClient.Name == client.Name &&
             employeeWhoWasClient.Surname == client.Surname &&
             employeeWhoWasClient.DateBirth == client.DateBirth &&
             employeeWhoWasClient.PassportId == client.PassportId &&
             employeeWhoWasClient is EmployeeDb;
Console.WriteLine("Результат преобразования клиента в сотрудника: "+result);
static void UpdateContactEmployeeBad(EmployeeDb employee)
{
    var descriptionContract = $"Компания Dex.\n" +
                              $"С сотрудником: {employee.Surname} {employee.Name},\n" +
                              $"cерия паспорта: {employee.PassportId},\n" +
                              $"заключён контракт.";
    employee.Contract = descriptionContract;
}

static void UpdateCurrencyBad(ref CurrencyDb currency)
{
    currency.Code = 304;
    currency.Name = "Юани";
}

static string CreateContractGood(string surname, string name, int passportId) =>
    $"Компания Dex.\n" +
    $"С сотрудником: {surname} {name},\n" +
    $"cерия паспорта: {passportId},\n" +
    $"заключён контракт.";

static CurrencyDb UpdateCurrencyGood(int code, string name) => new() { Code = code, Name = name };