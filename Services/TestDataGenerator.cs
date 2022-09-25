using Models;
using Bogus;
using Bogus.DataSets;

namespace Services;

public class TestDataGenerator
{
    private List<string> _currenciesName = new List<string>()
    {
        "usd","euro","dinar","lev","real","krone","dinar","ruble","hryvnia",
        "franc","yen","kuna","peso", "yane"
    };
    public List<ClientDb> GenerateListClient(int count)
    {
        var listClient = new List<ClientDb>();
        for (int i = 0; i < count; i++)
        {
            listClient.Add(GeneratingClient());
        }

        return listClient;
    }

    public Dictionary<int, ClientDb> GenerateDictionaryClient(int count)
    {
        var dictionaryClient = new Dictionary<int, ClientDb>();
        for (int i = 0; i < count; i++)
        {
            dictionaryClient.Add(i, GeneratingClient());
        }

        return dictionaryClient;
    }

    public List<EmployeeDb> GenerateListEmployee(int count)
    {
        var faker = new Faker("ru");
        var listEmployee = new List<EmployeeDb>();

        for (int i = 1; i < count; i++)
        {
            listEmployee.Add(GeneratingEmployee());
        }

        return listEmployee;
    }

    public Dictionary<ClientDb, AccountDb> GenerateDictionaryKeyClientValueAccount(int count)
    {
        var dictionaryClient = new Dictionary<ClientDb, AccountDb>();

        for (int i = 1; i < count; i++)
            dictionaryClient.Add(GeneratingClient(), GeneratingAccount());

        return dictionaryClient;
    }

    public Dictionary<ClientDb, List<AccountDb>> GenerateDictionaryKeyClientValueListAccount(int count)
    {
        var faker = new Faker("ru");
        var dictionaryClient = new Dictionary<ClientDb, List<AccountDb>>();

        for (int i = 1; i < count; i++)
        {
            dictionaryClient.Add(GeneratingClient(), GeneratingRandomNumberAccount());
        }

        return dictionaryClient;
    }

    public ClientDb GeneratingClient() =>
        new ClientDb()
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
            Accounts = new List<AccountDb>(){GeneratingAccount()}
        };


    public AccountDb GeneratingAccount() => new AccountDb()
    {
        CurrenciesDb = new List<CurrencyDb>(){GeneratingCurrency()},
            
        Amount = new Random().Next(0, 100000)
    };
    public CurrencyDb GeneratingCurrency() => new CurrencyDb
        {
            Code = new Random().Next(0, 2000),
            Name = _currenciesName[new Random().Next(0, 14)]
        };
    
    public EmployeeDb GeneratingEmployee() => new EmployeeDb()
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

    public List<AccountDb> GeneratingRandomNumberAccount()
    {
        var numberAccount = new Random().Next(0, 4);
        var list = new List<AccountDb>();
        for (int i = 0; i < numberAccount; i++)
        {
            list.Add(GeneratingAccount());
        }

        return list;
    }
}