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
        var dictionaryCurrency = dataGenerator.GenerateDictionaryKeyClientValueAccount(10);
        var client = new Client()
        {
            Surname = "Doe",
            Name = "John",
            DateBirth = new DateTime(1990, 4, 28, 13, 23, 6),
            PassportId = 123123,
            NumberPhone = 123123
        };
        //act
        
        //assert
        Assert.Pass();
    }
}