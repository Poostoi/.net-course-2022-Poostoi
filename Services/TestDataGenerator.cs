using Models;
using Bogus;
using Bogus.DataSets;
using Currency = Models.Currency;

namespace Services;

public class TestDataGenerator
{
    private List<string> _currenciesName = new List<string>()
    {
        "usd","euro","dinar","lev","real","krone","dinar","ruble","hryvnia",
        "franc","yen","kuna","peso", "yane"
    };
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
            DateBirth = new DateTime(
                new Random().Next(1900, 2000),
                new Random().Next(1, 12),
                new Random().Next(1, 28)
                ),
            NumberPhone = new Random().Next(111111, 999999),
            PassportId = new Random().Next(111111, 999999),
            Bonus = new Random().Next(1,1000)
        };


    public Account GeneratingAccount() => new Account()
    {
        Amount = new Random().Next(0, 100000)
    };
    public Currency GeneratingCurrency() => new Currency
        {
            Code = new Random().Next(0, 2000),
            Name = _currenciesName[new Random().Next(0, 14)]
        };
    
    public Employee GeneratingEmployee() => new Employee()
    {
        Surname = new Faker("ru").Name.FirstName(Name.Gender.Male),
        Name = new Faker("ru").Name.LastName(Name.Gender.Male),
        DateBirth = new DateTime(
            new Random().Next(1900, 2000),
            new Random().Next(1, 12),
            new Random().Next(1, 28)
        ),
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