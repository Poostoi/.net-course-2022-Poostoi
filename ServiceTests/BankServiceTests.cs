using Models;
using Services;
using Services.ExceptionCraft;

namespace ServiceTests;

public class BankServiceTest
{
    [Test]
    public void AddBonus_Client_BonusOne()
    {
        //arrange
        var bankService = new BankService<ClientDb>();
        var testDate = new TestDataGenerator();
        var client = testDate.GeneratingClient();
        //assert
        bankService.AddBonus(client);
        //act
        Assert.AreEqual(client.Bonus,1);
    }
    [Test]
    public void AddToBlackList_Client_BlackListContainsClientEmployee()
    {
        //arrange
        var bankService = new BankService<ClientDb>();
        var testDate = new TestDataGenerator();
        var client = testDate.GeneratingClient();
        var employee = testDate.GeneratingEmployee();
        //assert
        bankService.AddToBlackList<ClientDb>(client);
        bankService.AddToBlackList<EmployeeDb>(employee);
        //act
        Assert.True(bankService.BlackList[0] is ClientDb);
        Assert.True(bankService.BlackList[1] is EmployeeDb);
        
    }
    [Test]
    public void IsPersonInBlackList_Client_BlackListContainsClient()
    {
        //arrange
        var bankService = new BankService<ClientDb>();
        var testDate = new TestDataGenerator();
        var client = testDate.GeneratingClient();
        //assert
        bankService.AddToBlackList<ClientDb>(client);
        //act
        Assert.True(bankService.IsPersonInBlackList(client));
    }
}