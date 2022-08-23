namespace TradeCategoryApp.Classes
{
    public class HighRiskCategoryHandler : AbstractCategoryHandler
    {
        public override string Category => "HIGHRISK";
        public override string GetCategoryFromTrade(Trade trade)
        {
            if (trade.Value.CompareTo(1000000D) > 0 && trade.ClientSector.Equals("Private"))
                return Category;
                
            return base.GetCategoryFromTrade(trade);
        }
    }
}