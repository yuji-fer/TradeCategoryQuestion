using TradeCategoryApp.Interfaces;

namespace TradeCategoryApp.Classes
{
    public abstract class AbstractCategoryHandler : ICategoryHandler
    {
        private ICategoryHandler? _nextHandler;

        public abstract string Category { get; }

        public virtual string GetCategoryFromTrade(Trade trade)
        {
            if (this._nextHandler != null)
                return this._nextHandler.GetCategoryFromTrade(trade);
                
            return "NOCATEGORY";
        }

        public ICategoryHandler SetNext(ICategoryHandler handler)
        {
            this._nextHandler = handler;

            return handler;
        }
    }
}