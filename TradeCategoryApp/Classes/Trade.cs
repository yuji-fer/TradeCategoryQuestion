using TradeCategoryApp.Interfaces;

namespace TradeCategoryApp.Classes
{
    public class Trade : ITrade
    {
        private double _value;
        private string _sector = string.Empty;
        private DateTime _nextPaymentDate;
        
        public double Value { get => _value; set => _value = value; }

        public string ClientSector { get => _sector; set => _sector = value; }

        public DateTime NextPaymentDate { get => _nextPaymentDate; set => _nextPaymentDate = value; }
    }
}