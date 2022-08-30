using Models;

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
                NumberPhone = new Random().Next(111111,999999),
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
        var listEmployee = new List<Employee>();
        for (int i = 0; i < n; i++)
        {
            listEmployee.Add(new Employee()
            {
                Surname = "Doe",
                Name = "John",
                DateBirth = DateTime.Now.AddDays(new Random().Next(0,100000)),
                PassportId = 13123123,
                Contract = "Заключен",
                Salary = new Random().Next(2000,15000)
            });
        }

        return listEmployee;
    }
}