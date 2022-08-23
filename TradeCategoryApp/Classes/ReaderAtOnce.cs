using System.Globalization;
using TradeCategoryApp.Interfaces;

namespace TradeCategoryApp.Classes
{
    public class ReaderAtOnce : IReader
    {
        private readonly string _input;

        public ReaderAtOnce(string input)
        {
            _input = input ?? string.Empty;
        }

        public bool ProcessInput()
        {
            DateTime referenceDate;
			string? line;
			ushort numberOfTrades;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

			using (StringReader reader = new StringReader(_input))
            {
                if (!DateTime.TryParse(reader.ReadLine(), culture, DateTimeStyles.None, out referenceDate))
                    return false;

                if (!UInt16.TryParse(reader.ReadLine(), out numberOfTrades))
                    return false;

                List<String> categoriesOfTrades = new List<string>();
                TradeService tradeService = new TradeService();

                while ((line = reader.ReadLine()) != null)
                {
                    Trade? trade = tradeService.GetTradeFromInput(line);
                    if (trade == null)
                        continue;

                    categoriesOfTrades.Add(tradeService.GetTradeCategory(referenceDate, trade));
                }

                Console.WriteLine(string.Join('\n', categoriesOfTrades));

                return categoriesOfTrades.Count == numberOfTrades;
            }
        }
    }
}