using Migration;
using Services;

namespace ServiceTests;

public class CashDispenserServiceTests
{
    [Test]
    public void CashingOutInAccount_BankContext_True()
    {
        //arrange
        var bankContext = new BankContext();
        var token = new CancellationTokenSource();

        //act assert
        foreach (var task in WorkWithListTask(10, token, bankContext))
        {
            task.Wait();
        }

        token.Cancel();
    }

    private List<Task> WorkWithListTask(int countTask, CancellationTokenSource token, BankContext bankContext)
    {
        var accountsDbs = bankContext.Accounts.Take(10).ToList();
        var listTask = new List<Task>();
        for (int i = 0; i < countTask; i++)
        {
            foreach (var accountDb in accountsDbs)
            {
                listTask.Add(new CashDispenserService().CashingOutInAccount(token, bankContext, accountDb));
                Task.Delay(1000).Wait();
            }
        }

        return listTask;
    }
}