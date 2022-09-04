using Models;
using Bogus;
using Bogus.DataSets;
using Currency = Models.Currency;

namespace Services;

public class TestDataGenerator
{
    public List<Client> GenerateListClient(int count)
    {
        var listClient = new List<Client>();
        for (int i = 0; i < count; i++)
        {
            listClient.Add(GeneratingClient());
        }

        return listClient;
    }

    public Dictionary<int, Client> GenerateDictionaryClient(int count)
    {
        var dictionaryClient = new Dictionary<int, Client>();
        for (int i = 0; i < count; i++)
        {
            dictionaryClient.Add(i, GeneratingClient());
        }

        return dictionaryClient;
    }

    public List<Employee> GenerateListEmployee(int count)
    {
        var faker = new Faker("ru");
        var listEmployee = new List<Employee>();

        for (int i = 1; i < count; i++)
        {
            listEmployee.Add(GeneratingEmployee());
        }

        return listEmployee;
    }

    public Dictionary<Client, Account> GenerateDictionaryKeyClientValueAccount(int count)
    {
        var dictionaryClient = new Dictionary<Client, Account>();

        for (int i = 1; i < count; i++)
            dictionaryClient.Add(GeneratingClient(), GeneratingAccount());

        return dictionaryClient;
    }

    public Dictionary<Client, List<Account>> GenerateDictionaryKeyClientValueListAccount(int count)
    {
        var faker = new Faker("ru");
        var dictionaryClient = new Dictionary<Client, List<Account>>();

        for (int i = 1; i < count; i++)
        {
            dictionaryClient.Add(GeneratingClient(), GeneratingRandomNumberAccount());
        }

        return dictionaryClient;
    }

    public Client GeneratingClient() =>
        new Client()
        {
            Surname = new Faker("ru").Name.FirstName(Name.Gender.Male),
            Name = new Faker("ru").Name.LastName(Name.Gender.Male),
            DateBirth = new Faker("ru").Date.Past(new Random().Next(0, 55)),
            NumberPhone = new Random().Next(111111, 999999),
            PassportId = new Random().Next(111111, 999999)
        };


    public Account GeneratingAccount() => new Account()
    {
        Currency = new Currency
        {
            Code = new Random().Next(0, 2000),
            Name = "usd"
        },
        Amount = new Random().Next(0, 100000)
    };
    public Employee GeneratingEmployee() => new Employee()
    {
        Surname = new Faker("ru").Name.FirstName(Name.Gender.Male),
        Name = new Faker("ru").Name.LastName(Name.Gender.Male),
        DateBirth = new Faker("ru").Date.Past(new Random().Next(0, 55)),
        PassportId = new Random().Next(111111, 999999),
        Contract = "Заключен",
        Salary = new Random().Next(2000, 15000)
    };

    public List<Account> GeneratingRandomNumberAccount()
    {
        var numberAccount = new Random().Next(0, 4);
        var list = new List<Account>();
        for (int i = 0; i < numberAccount; i++)
        {
            list.Add(GeneratingAccount());
        }

        return list;
    }
}