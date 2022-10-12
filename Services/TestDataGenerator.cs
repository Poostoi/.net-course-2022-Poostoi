using Models;
using Bogus;
using Bogus.DataSets;
using Currency = Models.Currency;

namespace Services;

public class TestDataGenerator
{
    private List<string> _currenciesName = new List<string>()
    {
        "usd", "euro", "dinar", "lev", "real", "krone", "dinar", "ruble", "hryvnia",
        "franc", "yen", "kuna", "peso", "yane"
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

    public Client GeneratingClient()
    {
        
        var client = new Client()
        {
            Id = Guid.NewGuid(),
            Surname = new Faker("ru").Name.FirstName(Name.Gender.Male),
            Name = new Faker("ru").Name.LastName(Name.Gender.Male),
            DateBirth = new DateTime(
                new Random().Next(1900, 2000),
                new Random().Next(1, 12),
                new Random().Next(1, 28)
            ),
            NumberPhone = new Random().Next(111111, 999999),
            PassportId = new Random().Next(111111, 999999),
            Bonus = new Random().Next(1, 1000)
        };
        var account = GeneratingAccount(client);
        client.Accounts = new List<Account>()
        {
            account
        };

        return client;
    }

    public Client GeneratingClient(Account account)
    {
        var client = new Client()
        {
            Id = Guid.NewGuid(),
            Accounts = new List<Account>()
            {
                account
            },
            Surname = new Faker("ru").Name.FirstName(Name.Gender.Male),
            Name = new Faker("ru").Name.LastName(Name.Gender.Male),
            DateBirth = new DateTime(
                new Random().Next(1900, 2000),
                new Random().Next(1, 12),
                new Random().Next(1, 28)
            ),
            NumberPhone = new Random().Next(111111, 999999),
            PassportId = new Random().Next(111111, 999999),
            Bonus = new Random().Next(1, 1000)
        };

        return client;
    }


    public Account GeneratingAccount()
    {
        var client = GeneratingClient();
        var currency = GeneratingCurrency();
        var account = new Account()
        {
            Id = Guid.NewGuid(),
            Currency = currency,
            CurrencyId = currency.Id,
            Amount = new Random().Next(0, 100000),
            Client = client,
            ClientId = client.Id
        };
        return account;
    }
    public Account GeneratingAccount(Client client)
    {
        var currency = GeneratingCurrency();
        var account = new Account()
        {
            Id = Guid.NewGuid(),
            Currency = currency,
            CurrencyId = currency.Id,
            Amount = new Random().Next(0, 100000),
            Client = client,
            ClientId = client.Id
        };
        return account;
    }

    public Currency GeneratingCurrency() => new Currency
    {
        Id = Guid.NewGuid(),
        Code = new Random().Next(0, 2000),
        Name = _currenciesName[new Random().Next(0, 14)]
    };

    public Employee GeneratingEmployee() => new Employee()
    {
        Id = Guid.NewGuid(),
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