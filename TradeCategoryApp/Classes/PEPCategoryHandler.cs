namespace TradeCategoryApp.Classes
{
    public class PEPCategoryHandler : AbstractCategoryHandler
    {
        public override string Category => "PEP";

        public override string GetCategoryFromTrade(Trade trade)
        {
            if (trade.IsPoliticallyExposed)
                return Category;
                
            return base.GetCategoryFromTrade(trade);
        }
    }
}