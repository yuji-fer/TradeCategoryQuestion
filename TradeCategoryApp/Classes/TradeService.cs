using System.Globalization;

namespace TradeCategoryApp.Classes
{
    public class TradeService
    {
        public Trade? GetTradeFromInput(string input)
        {
            if (input == null)
                return null;

            double amount;
            DateTime nextPaymentDate;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            string[] props = input.Split(' ');

            if (props.Length < 3)
                return null;
            
            string sector = props[1];

            if (!Double.TryParse(props[0], out amount))
                return null;
            if (!DateTime.TryParse(props[2], culture, DateTimeStyles.None, out nextPaymentDate))
                return null;
            if (!ValidSector(sector))
                return null;
            
            Trade trade = new Trade
            {
                Value = amount,
                ClientSector = sector,
                NextPaymentDate = nextPaymentDate
            };

            return trade;
        }

        public bool ValidSector(string sector)
        {
            if (sector.Equals("Private") || sector.Equals("Public"))
                return true;
            
            return false;
        }

        public string GetTradeCategory(DateTime referenceDate, Trade trade)
        {
            if (trade == null)
                return string.Empty;
                
            AbstractCategoryHandler handler = new ExpiredCategoryHandler(referenceDate);
            handler.SetNext(new HighRiskCategoryHandler())
                   .SetNext(new MediumRiskCategoryHandler());

            return handler.GetCategoryFromTrade(trade);
        }
    }
}