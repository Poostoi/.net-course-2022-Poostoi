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
        var client = dataGenerator.GeneratingClient();
        var account = dataGenerator.GeneratingAccount();
        
        //act
        dictionaryAccount.Add(client, account);
        var expected = dictionaryAccount[client].Equals(account);

        //assert 
        Assert.True(expected);
    }
    [Test]
    public void GenerateDictionaryKeyClientValueListAccount_Client_ListAccount()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var dictionaryAccount = dataGenerator.GenerateDictionaryKeyClientValueListAccount(10);
        var client = dataGenerator.GeneratingClient();
        var accounts = dataGenerator.GeneratingRandomNumberAccount();
        //act
        dictionaryAccount.Add(client, accounts);
        var expected = dictionaryAccount[client].Equals(accounts);

        //assert 
        Assert.True(expected);
    }
    
    
    [Test]
    public void EqualsEmployee_Employee_True()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var listEmployee = dataGenerator.GenerateListEmployee(10);
        var employee = dataGenerator.GeneratingEmployee();
        //act
        listEmployee.Add(employee);
        var expected = listEmployee.Find(e => e.Equals(employee));

        //assert 
        Assert.NotNull(expected);
    }
}