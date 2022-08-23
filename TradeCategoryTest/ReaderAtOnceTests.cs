using TradeCategoryApp.Classes;

namespace TradeCategoryTest
{
    public class ReaderAtOnceTests
    {
        private ReaderAtOnce? _readerAtOnce;

        private void CreateInstance(string input)
        {
            _readerAtOnce = new ReaderAtOnce(input);
        }

        [
            Theory,
            InlineData("12/11/2020\n4\n2000000 Private 12/29/2025\n400000 Public 07/01/2020\n5000000 Public 01/02/2024\n3000000 Public 10/26/2023")
        ]
        public void ProcessInput_InputCorrect_ReturnTrue(string input)
        {
            CreateInstance(input);
            bool result = _readerAtOnce!.ProcessInput();
            Assert.True(result, "The process must be OK");
        }

        [Fact]
        public void ProcessInput_InputNull_ReturnFalse()
        {
            CreateInstance(null);
            bool result = _readerAtOnce!.ProcessInput();
            Assert.False(result, "The process must be NOK");
        }

        [
            Theory,
            InlineData("13/11/2020\n4\n2000000 Private 12/29/2025\n400000 Public 07/01/2020\n5000000 Public 01/02/2024\n3000000 Public 10/26/2023"),
            InlineData("aaa\n4\n2000000 Private 12/29/2025\n400000 Public 07/01/2020\n5000000 Public 01/02/2024\n3000000 Public 10/26/2023"),
            InlineData("2020\n4\n2000000 Private 12/29/2025\n400000 Public 07/01/2020\n5000000 Public 01/02/2024\n3000000 Public 10/26/2023")
        ]
        public void ProcessInput_InputFirstLineNotIsValidDate_ReturnFalse(string input)
        {
            CreateInstance(input);
            bool result = _readerAtOnce!.ProcessInput();
            Assert.False(result, "The First Line is a valid date");
        }

        [
            Theory,
            InlineData("12/11/2020\naaa\n2000000 Private 12/29/2025\n400000 Public 07/01/2020\n5000000 Public 01/02/2024\n3000000 Public 10/26/2023"),
            InlineData("12/11/2020\n13/11/2020\n2000000 Private 12/29/2025\n400000 Public 07/01/2020\n5000000 Public 01/02/2024\n3000000 Public 10/26/2023")
        ]
        public void ProcessInput_InputSecondLineNAN_ReturnFalse(string input)
        {
            CreateInstance(input);
            bool result = _readerAtOnce!.ProcessInput();
            Assert.False(result, "The Second Line is a number");
        }

        [
            Theory,
            InlineData("12/11/2020\n5\n2000000 Private 12/29/2025\n400000 Public 07/01/2020\n5000000 Public 01/02/2024\n3000000 Public 10/26/2023"),
            InlineData("12/11/2020\n2\n2000000 Private 12/29/2025\n400000 Public 07/01/2020\n5000000 Public 01/02/2024\n3000000 Public 10/26/2023")
        ]
        public void ProcessInput_InputSecondLineNotMatchWithNextNLines_ReturnFalse(string input)
        {
            CreateInstance(input);
            bool result = _readerAtOnce!.ProcessInput();
            Assert.False(result, "The Second Line match with next N lines");
        }

        [
            Theory,
            InlineData("12/11/2020\n2\n2000000 Private 12/29/2025\n400000 Public")
        ]
        public void ProcessInput_AnyFromNextNLinesNotIsValid_ReturnFalse(string input)
        {
            CreateInstance(input);
            bool result = _readerAtOnce!.ProcessInput();
            Assert.False(result, "Any from next N lines should be not valid");
        }
    }
}