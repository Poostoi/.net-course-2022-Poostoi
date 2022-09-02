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
        var expected = dictionaryCurrency[client] is Account;

        //assert 
        Assert.True(expected);
    }
    [Test]
    public void Equals_CheckWork_True()
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
        Client expected = new Client();
        //act
        foreach (var element in dictionaryCurrency)
        {
            if (element.Key.Equals(client))
                expected = element.Key;
        }

        //assert 
        Assert.NotNull(expected);
    }
}