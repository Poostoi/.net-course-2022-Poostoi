using Services;

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
        var response = await currencyService.ConvertCurrency(currency);
        //assert 
        
        Assert.NotNull(response);
    }
}