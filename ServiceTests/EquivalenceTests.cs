using Models;
using Services;

namespace ServiceTests;

public class EquivalenceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetHashCodeNecessityPositivTest()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var dictionaryAccount = dataGenerator.GenerateDictionaryKeyClientValueAccount(10);
        var client = new Client()
        {
            Surname = "Doe",
            Name = "John",
            DateBirth = new DateTime(1990, 4, 28, 13, 23, 6),
            PassportId = 123123,
            NumberPhone = 123123
        };
        
        //act
        var expected = dictionaryAccount[client] is Account;

        //assert 
        Assert.True(expected);
    }
    [Test]
    public void GenerateDictionaryKeyClientValueListAccount_Client_ListAccount()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var dictionaryAccount = dataGenerator.GenerateDictionaryKeyClientValueListAccount(10);
        var client = new Client()
        {
            Surname = "Doe",
            Name = "John",
            DateBirth = new DateTime(1990, 4, 28, 13, 23, 6),
            PassportId = 123123,
            NumberPhone = 123123
        };
        
        //act
        var expected = dictionaryAccount[client] is List<Account>;

        //assert 
        Assert.True(expected);
    }
    [Test]
    public void Equals_CheckWork_True()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var dictionaryAccount = dataGenerator.GenerateDictionaryKeyClientValueAccount(10);
        var client = new Client()
        {
            Surname = "Doe",
            Name = "John",
            DateBirth = new DateTime(1990, 4, 28, 13, 23, 6),
            PassportId = 123123,
            NumberPhone = 123123
        };
        Client expected = new Client();
        //act
        foreach (var element in dictionaryAccount)
        {
            if (element.Key.Equals(client))
                expected = element.Key;
        }

        //assert 
        Assert.NotNull(expected);
    }
    [Test]
    public void EqualsEmployee_Employee_True()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var listEmployee = dataGenerator.GenerateListEmployee(10);
        var employee = new Employee()
        {
            Surname = "Doe",
            Name = "John",
            DateBirth = new DateTime(1990, 4, 28, 13, 23, 6),
            PassportId = 123123,
            Contract = "Заключён",
            Salary = 2000
        };
        //act
        var expected = employee.Equals(listEmployee[0]);

        //assert 
        Assert.True(expected);
    }
}