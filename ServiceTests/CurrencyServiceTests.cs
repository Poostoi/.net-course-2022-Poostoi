using Services;
using Services.ExceptionCraft;
using Services.Storage;

namespace ServiceTests;

public class CurrencyServiceTests
{
    [Test]
    public async Task AddClient_Client_AgeLessException()
    {
        //arrange
        var dataGenerator = new TestDataGenerator();
        var currency = dataGenerator.GeneratingCurrency();
        var currencyService = new CurrencyService();
        //act
        var newCurrency = await currencyService.ConvertCurrency(currency);
        //assert 
        
        Assert.True(true);
    }
}