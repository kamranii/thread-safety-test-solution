using Solution_IndigoSoft.Interfaces;
using Solution_IndigoSoft.Models;

Console.WriteLine("Тестирование ReaderWriterLockSlim:");
RunTest(new Solution_IndigoSoft.ReaderWriteLockSlimSolution.ReaderWriterLockSlimSolutionClass.RatesStorage());

Console.WriteLine("\nТестирование Lock:");
RunTest(new Solution_IndigoSoft.GlobalMutexSolution.GlobalMutexSolutionClass.RatesStorage());

static void RunTest(IRatesStorage ratesStorage)
{
    var initialRates = new List<NativeRate>
    {
        new NativeRate { Time = DateTime.Now, Symbol = "EURUSD", Bid = 1.1000, Ask = 1.1005 },
        new NativeRate { Time = DateTime.Now, Symbol = "USDJPY", Bid = 109.50, Ask = 109.55 },
        new NativeRate { Time = DateTime.Now, Symbol = "GBPUSD", Bid = 1.3000, Ask = 1.3005 }
    };

    var updateTask = Task.Run(() =>
    {
        foreach (var rate in initialRates)
        {
            for (int i = 0; i < 10; i++)
            {
                ratesStorage.UpdateRate(rate);
                Thread.Sleep(50);
            }
        }
    });

    var readTask = Task.Run(() =>
    {
        for (int i = 0; i < 30; i++)
        {
            var rate = ratesStorage.GetRate("EURUSD");
            if (rate != null)
            {
                Console.WriteLine($"Получены котировки: {rate.Symbol} - Bid: {rate.Bid}, Ask: {rate.Ask}");
            }
            Thread.Sleep(50);
        }
    });

    Task.WaitAll(updateTask, readTask);
}

