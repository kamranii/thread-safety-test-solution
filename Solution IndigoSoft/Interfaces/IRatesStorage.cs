using Solution_IndigoSoft.Models;

namespace Solution_IndigoSoft.Interfaces
{
	public interface IRatesStorage
	{
        void UpdateRate(NativeRate newRate);
        Rate GetRate(string symbol);
    }
}

