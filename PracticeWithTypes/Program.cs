using Models;

//Bad change employee
var employeeBad = new Employee("Ноур", "Олег", 42342342, DateTime.Now);
UpdateContactEmployeeBad(employeeBad);
Console.WriteLine($"(Bad)Контракт c серией паспорта {employeeBad.PassportId}: \n{employeeBad.Contract}");

//bad change currency
var currencyBad = new Currency(221, "Евро");
UpdateCurrencyBad(ref currencyBad);
Console.WriteLine($"\n(Bad)Код валюты: {currencyBad.Code},\n" +
                  $"Название валюты: {currencyBad.Name}.");
//good change employee
var employeeGood = new Employee("Гурбанова", "Эсьмира", 52632342, DateTime.Now);
employeeGood.Contract = CreateContractGood(employeeGood.Surname, employeeGood.Name, employeeGood.PassportId);
Console.WriteLine($"\n(Good)Контракт c серией паспорта {employeeGood.PassportId}: \n{employeeGood.Contract}");
//good change currency
var currencyGood = new Currency(334, "Ены");
currencyGood = UpdateCurrencyGood(545, "Кроны");
Console.WriteLine($"\n(Good)Код валюты: {currencyGood.Code},\n" +
                  $"Название валюты: {currencyGood.Name}.");

static void UpdateContactEmployeeBad(Employee employee)//(BAD)метод неправильного изменения контракта сотрудника
{
    var descriptionContract = $"Компания Dex.\n" +
                              $"С сотрудником: {employee.Surname} {employee.Name},\n" +
                              $"cерия паспорта: {employee.PassportId},\n" +
                              $"заключён контракт.";
    employee.Contract = descriptionContract;
}

static void UpdateCurrencyBad(ref Currency currency)//(BAD)работающий метод неправильного изменения валюты
{
    currency.Code = 304;
    currency.Name = "Юани";
}
static string CreateContractGood(string surname, string name, int passportId) => //(Good)правильный(по идее) подход изменения конктракта
    $"Компания Dex.\n" +
    $"С сотрудником: {surname} {name},\n" +
    $"cерия паспорта: {passportId},\n" +
    $"заключён контракт.";

static Currency UpdateCurrencyGood(int code, string name) => new Currency(code, name); //(Good)правильный подход к обнавлению сощности валюты



