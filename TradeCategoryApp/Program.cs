using TradeCategoryApp.Classes;
using TradeCategoryApp.Interfaces;

namespace TradeCategoryApp
{
    class Program
	{
		static void Main(string[] args)
		{
			IReader reader = new ReaderAtOnce(args[0]);
			bool result = reader.ProcessInput();
		}
	}
}
