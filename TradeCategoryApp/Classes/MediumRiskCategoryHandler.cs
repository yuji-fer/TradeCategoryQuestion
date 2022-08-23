namespace TradeCategoryApp.Classes
{
    public class MediumRiskCategoryHandler : AbstractCategoryHandler
    {
        public override string Category => "MEDIUMRISK";
        public override string GetCategoryFromTrade(Trade trade)
        {
            if (trade.Value.CompareTo(1000000D) > 0 && trade.ClientSector.Equals("Public"))
                return Category;
                
            return base.GetCategoryFromTrade(trade);
        }
    }
}