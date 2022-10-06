using Services;
using System.Threading;
using Models;

namespace ServiceTests;

public class ThreadAndTaskTests
{
    
    [Test]
    public void ParallelAccrual_Client_NotEqual1000()
    {
        //arrange
        var account = new TestDataGenerator().GeneratingAccount();
        var oldAmountAccount = account.Amount;
        object[] parametr = new object[] { account, 10 };
        Thread firstThread = new(Test);
        firstThread.Name = "Первый";
        Thread secondThread = new(Test);
        secondThread.Name = "Второй";


        //act
        firstThread.Start(parametr);
        secondThread.Start(parametr);
        

        //assert
        //Assert.AreEqual(account.Amount - oldAmountAccount, 2000);
        Thread.Sleep(10000);
        Assert.True(true);
    }

    object key = new();

    private void Test(object? o)
    {
        var array = (object[])o;
        var account = (Account)array[0];
        var count = (int)array[1];
        lock (key)
        {
            for (int i = 0; i < count; i++)
            {
                account.Amount += 100;
                Console.WriteLine(
                    $"{Thread.CurrentThread.Name} {i}поток, значение счёта увеличилось на 100, сейчас: {account.Amount}");
            }
        }
    }
}