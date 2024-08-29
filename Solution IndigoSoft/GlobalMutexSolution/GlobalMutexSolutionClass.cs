using Solution_IndigoSoft.Interfaces;
using Solution_IndigoSoft.Models;

namespace Solution_IndigoSoft.GlobalMutexSolution
{
    /* Объяснение:

        Простой lock используется для гарантии, что методы UpdateRate и GetRate не могут выполняться одновременно несколькими потоками.
        Это гарантирует, что чтение и запись не могут происходить одновременно.

        Плюсы:
        Прост в реализации.
        Легко понять и поддерживать.

        Минусы:
        Менее производителен из-за блокировки как при чтении, так и при записи.
        Не подходит для сценариев с высокой конкуренцией. 
    */
    public class GlobalMutexSolutionClass
	{
        public class RatesStorage: IRatesStorage
        {
            private readonly Dictionary<string, Rate> rates = new();
            private readonly object lockObject = new();

            public void UpdateRate(NativeRate newRate)
            {
                Console.WriteLine("GlobalMutex");
                lock (lockObject)
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
            }

            public Rate GetRate(string symbol)
            {
                lock (lockObject)
                {
                    if (rates.ContainsKey(symbol))
                        return null;
                    return rates[symbol];
                }
            }
        }
    }
}

