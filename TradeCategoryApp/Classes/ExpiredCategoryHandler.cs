namespace TradeCategoryApp.Classes
{
    public class ExpiredCategoryHandler : AbstractCategoryHandler
    {
        private readonly DateTime _referenceDate;
        public ExpiredCategoryHandler(DateTime referenceDate)
        {
            _referenceDate = referenceDate;
        }

        public override string Category => "EXPIRED";
        public override string GetCategoryFromTrade(Trade trade)
        {
            if (trade.NextPaymentDate.AddDays(30).CompareTo(_referenceDate) < 0)
                return Category;
                
            return base.GetCategoryFromTrade(trade);
        }
    }
}