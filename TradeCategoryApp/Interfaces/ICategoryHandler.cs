using TradeCategoryApp.Classes;

namespace TradeCategoryApp.Interfaces
{
    public interface ICategoryHandler
    {
        ICategoryHandler SetNext(ICategoryHandler handler);

        string GetCategoryFromTrade(Trade trade);
    }
}