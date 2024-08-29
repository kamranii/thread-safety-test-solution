using Solution_IndigoSoft.Interfaces;
using Solution_IndigoSoft.Models;

namespace Solution_IndigoSoft.ReaderWriteLockSlimSolution
{
    /*
     * Объяснение:

        ReaderWriterLockSlim позволяет нескольким потокам читать одновременно, но обеспечивает эксклюзивный доступ для записи.
        EnterWriteLock и ExitWriteLock гарантируют, что метод UpdateRate имеет эксклюзивный доступ во время обновления.
        EnterReadLock и ExitReadLock гарантируют безопасное чтение GetRate при одновременном чтении несколькими потоками.

        Плюсы:
        Тонкий контроль над операциями чтения и записи.
        Эффективно, когда операций чтения больше, чем операций записи.

        Минусы:
        Дополнительная сложность управления блокировками.
        Некоторая нагрузка, связанная с получением и освобождением блокировок.
    */

    public class ReaderWriterLockSlimSolutionClass
	{
        public class RatesStorage: IRatesStorage
        {
            private readonly Dictionary<string, Rate> rates = new();
            private readonly ReaderWriterLockSlim lockSlim = new();

            public void UpdateRate(NativeRate newRate)
            {
                Console.WriteLine("ReaderWriterLock");
                lockSlim.EnterWriteLock();
                try
                {
                    if (!rates.ContainsKey(newRate.Symbol))
                    {
                        rates[newRate.Symbol] = new Rate();
                    }

                    var rate = rates[newRate.Symbol];
                    rate.Time = newRate.Time;
                    rate.Bid = newRate.Bid;
                    rate.Ask = newRate.Ask;
                }
                finally
                {
                    lockSlim.ExitWriteLock();
                }
            }

            public Rate GetRate(string symbol)
            {
                lockSlim.EnterReadLock();
                try
                {
                    if (!rates.ContainsKey(symbol))
                        return null;
                    return rates[symbol];
                }
                finally
                {
                    lockSlim.ExitReadLock();
                }
            }
        }
    }
}

