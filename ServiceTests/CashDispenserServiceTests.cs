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

    [Test]
    public async Task Test()
    {
        ThreadPool.SetMaxThreads(10, 10);
        ThreadPool.GetAvailableThreads(out var worker, out var completition);
        Console.WriteLine(worker.ToString());
        var token = new CancellationTokenSource();
        var listTask = new List<Task>();
        for (int i = 0; i < 5; i++)
        {
            listTask.Add( Test2(token, worker));
            Console.WriteLine($"{worker.ToString()}");
            Task.Delay(1000).Wait();
        }
    }

    private  Task Test2(CancellationTokenSource token, int worker)
    {
        return  Task.Run(() =>
        {
            while (!token.IsCancellationRequested)
            {
                if (true)
                {
                    //Task.Delay(5000).Wait();
                }

                Console.WriteLine($"{worker.ToString()}");
            }
        });
    }


    private List<Task> WorkWithListTask(int countTask, CancellationTokenSource token, BankContext bankContext)
    {
        var accountsDbs = bankContext.Accounts.Take(10).ToList();
        var listTask = new List<Task>();
        ThreadPool.SetMaxThreads(10, 10);
        ThreadPool.GetAvailableThreads(out var worker, out var completition);

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