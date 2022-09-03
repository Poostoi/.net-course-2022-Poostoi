using Models;
using Bogus;
using Bogus.DataSets;
using Currency = Models.Currency;

namespace Services;

public class TestDataGenerator
{
    public List<Client> GenerateListClient(int n)
    {
        var listClient = new List<Client>();
        for (int i = 0; i < n; i++)
        {
            listClient.Add(new Client()
            {
                Surname = "Doe",
                Name = "John",
                DateBirth = DateTime.Now,
                NumberPhone = new Random().Next(111111, 999999),
                PassportId = 13123123
            });
        }

        return listClient;
    }

    public Dictionary<int, Client> GenerateDictionaryClient(int n)
    {
        var dictionaryClient = new Dictionary<int, Client>();
        for (int i = 0; i < n; i++)
        {
            dictionaryClient.Add(i, new Client()
            {
                Surname = "Doe",
                Name = "John",
                DateBirth = DateTime.Now,
                NumberPhone = i,
                PassportId = 13123123
            });
        }

        return dictionaryClient;
    }

    public List<Employee> GenerateListEmployee(int n)
    {
        var faker = new Faker("ru");
        var listEmployee = new List<Employee>();
        listEmployee.Add(
            new Employee()
            {
                Surname = "Doe",
                Name = "John",
                DateBirth = new DateTime(1990, 4, 28, 13, 23, 6),
                PassportId = 123123,
                Contract = "Заключён",
                Salary = 2000
            });
        for (int i = 1; i < n; i++)
        {
            listEmployee.Add(new Employee()
            {
                Surname = faker.Name.FirstName(Name.Gender.Male),
                Name = faker.Name.LastName(Name.Gender.Male),
                DateBirth = faker.Date.Past(new Random().Next(0, 55)),
                PassportId = new Random().Next(111111, 999999),
                Contract = "Заключен",
                Salary = new Random().Next(2000, 15000)
            });
        }

        return listEmployee;
    }

    public Dictionary<Client, Account> GenerateDictionaryKeyClientValueAccount(int n)
    {
        var faker = new Faker("ru");
        var dictionaryClient = new Dictionary<Client, Account>();
        dictionaryClient.Add(
            new Client()
            {
                Surname = "Doe",
                Name = "John",
                DateBirth = new DateTime(1990, 4, 28, 13, 23, 6),
                PassportId = 123123,
                NumberPhone = 123123
            }, new Account()
            {
                Currency = new Currency
                {
                    Code = new Random().Next(0, 2000),
                    Name = "usd"
                },
                Amount = new Random().Next(0, 100000)
            });


        for (int i = 1; i < n; i++)
        {
            dictionaryClient.Add(new Client()
            {
                Surname = faker.Name.FirstName(Name.Gender.Male),
                Name = faker.Name.LastName(Name.Gender.Male),
                DateBirth = faker.Date.Past(new Random().Next(0, 55)),
                NumberPhone = new Random().Next(111111, 999999),
                PassportId = new Random().Next(111111, 999999)
            }, new Account()
            {
                Currency = new Currency
                {
                    Code = new Random().Next(0, 2000),
                    Name = "usd"
                },
                Amount = new Random().Next(0, 100000)
            });
        }

        return dictionaryClient;
    }

    public Dictionary<Client, List<Account>> GenerateDictionaryKeyClientValueListAccount(int n)
    {
        var faker = new Faker("ru");
        var dictionaryClient = new Dictionary<Client, List<Account>>();
        dictionaryClient.Add(
            new Client()
            {
                Surname = "Doe",
                Name = "John",
                DateBirth = new DateTime(1990, 4, 28, 13, 23, 6),
                PassportId = 123123,
                NumberPhone = 123123
            }, GeneratingRandomNumberAccount());


        for (int i = 1; i < n; i++)
        {
            dictionaryClient.Add(new Client()
            {
                Surname = faker.Name.FirstName(Name.Gender.Male),
                Name = faker.Name.LastName(Name.Gender.Male),
                DateBirth = faker.Date.Past(new Random().Next(0, 55)),
                NumberPhone = new Random().Next(111111, 999999),
                PassportId = new Random().Next(111111, 999999)
            }, GeneratingRandomNumberAccount());
        }
        return dictionaryClient;
    }
    private  List<Account> GeneratingRandomNumberAccount()
    {
        var numberAccount = new Random().Next(0, 4);
        var list = new List<Account>();
        for (int i = 0; i < numberAccount; i++)
        {
            list.Add(new Account() { Currency = new Currency { Code = new Random().Next(0, 2000), Name = "usd" }, Amount = new Random().Next(0, 100000) });
        }
        return list;
    }
}