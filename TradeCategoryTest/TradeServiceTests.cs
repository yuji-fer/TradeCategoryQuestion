using TradeCategoryApp.Classes;

namespace TradeCategoryTest
{
    public class TradeServiceTests
    {
        readonly TradeService _tradeService;

        public TradeServiceTests() => _tradeService = new TradeService();

        private readonly DateTime _referenceDate = new DateTime(2020, 12, 11);

        private Trade CreateFakeTrade(
            bool expired = false,
            bool publicSector = false,
            bool valueLessThan1Million = false)
        {
            return new Trade
            {
                Value = (valueLessThan1Million ? 400000 : 2000000),
                ClientSector = (publicSector ? "Public" : "Private"),
                NextPaymentDate = _referenceDate.AddDays((expired ? -31 : 1))
            };
        }

        [
            Theory,
            InlineData("2000000 Private 12/29/2025", 2000000),
            InlineData("400000 Public 07/01/2020", 400000),
            InlineData("5000000 Public 01/02/2024", 5000000),
            InlineData("3000000 Public 10/26/2023", 3000000)
        ]
        public void GetTradeFromInput_InputCorrect_ReturnTrade(string input, double valueExpected)
        {
            Trade? trade = _tradeService.GetTradeFromInput(input);
            Assert.NotNull(trade);
            Assert.Equal(valueExpected, trade?.Value);
        }

        [Fact]
        public void GetTradeFromInput_InputNull_ReturnNull()
        {
            Trade? trade = _tradeService.GetTradeFromInput(null);
            Assert.Null(trade);
        }

        [
            Theory,
            InlineData("aaa Private 12/29/2025"),
            InlineData("07/01/2020 Public 07/01/2020")
        ]
        public void GetTradeFromInput_FirstColumnNAN_ReturnNull(string input)
        {
            Trade? trade = _tradeService.GetTradeFromInput(input);
            Assert.Null(trade);
        }

        [
            Theory,
            InlineData("2000000 aaa 12/29/2025"),
            InlineData("400000 123 07/01/2020"),
            InlineData("5000000 01/02/2024 01/02/2024")
        ]
        public void GetTradeFromInput_SecondColumnNotIsValidSector_ReturnNull(string input)
        {
            Trade? trade = _tradeService.GetTradeFromInput(input);
            Assert.Null(trade);
        }

        [
            Theory,
            InlineData("2000000 Private 13/29/2025"),
            InlineData("400000 Public aaa"),
            InlineData("5000000 Public 123")
        ]
        public void GetTradeFromInput_ThirdColumnNotIsValidDate_ReturnNull(string input)
        {
            Trade? trade = _tradeService.GetTradeFromInput(input);
            Assert.Null(trade);
        }

        [
            Theory,
            InlineData("2000000 Private"),
            InlineData("400000"),
            InlineData("")
        ]
        public void GetTradeFromInput_InputWithLessThan3Columns_ReturnNull(string input)
        {
            Trade? trade = _tradeService.GetTradeFromInput(input);
            Assert.Null(trade);
        }

        [
            Theory,
            InlineData("Private"),
            InlineData("Public")
        ]
        public void ValidSector_ValidSectors_ReturnTrue(string sector)
        {
            bool result = _tradeService.ValidSector(sector);
            Assert.True(result, "The result must be True");
        }

        [
            Theory,
            InlineData("123"),
            InlineData("12/29/2025"),
            InlineData("aaa"),
            InlineData("")
        ]
        public void ValidSector_InvalidSectors_ReturnFalse(string sector)
        {
            bool result = _tradeService.ValidSector(sector);
            Assert.False(result, "The result must be False");
        }

        [
            Theory,
            InlineData(true, false, "EXPIRED"),
            InlineData(false, false, "HIGHRISK"),
            InlineData(false, true, "MEDIUMRISK")
        ]
        public void GetTradeCategory_ExistingCategories_ReturnCategory(
            bool expired,
            bool publicSector,
            string expected)
        {
            Trade trade = CreateFakeTrade(expired, publicSector);
            string result = _tradeService.GetTradeCategory(_referenceDate, trade);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetTradeCategory_NoCategory_ReturnNoCategory()
        {
            Trade trade = CreateFakeTrade(valueLessThan1Million: true);
            string result = _tradeService.GetTradeCategory(_referenceDate, trade);
            Assert.Equal("NOCATEGORY", result);
        }

        [Fact]
        public void GetTradeCategory_TradeNull_ReturnEmptyString()
        {
            string result = _tradeService.GetTradeCategory(DateTime.Now, null);
            Assert.Equal(string.Empty, result);
        }
    }
}