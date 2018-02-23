using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataCaseSensitiveStringValueComparerTests
    {
        private TableDataCaseSensitiveStringValueComparer comparer;

        public TableDataCaseSensitiveStringValueComparerTests()
        {
            comparer = new TableDataCaseSensitiveStringValueComparer();
        }

        [Fact]
        public void TableDataCaseSensitiveStringValueComparerIsMatchTrue()
        {
            string x = "a";
            string y = "a";

            bool actual = comparer.IsMatch(x, y);

            Assert.True(actual);
        }

        [Fact]
        public void TableDataCaseSensitiveStringValueComparerIsMatchTrueForInteger()
        {
            int x = 1;
            string y = "1";

            bool actual = comparer.IsMatch(x, y);

            Assert.True(actual);
        }

        [Fact]
        public void TableDataCaseSensitiveStringValueComparerIsMatchFalse()
        {
            string x = "a";
            string y = "b";

            bool actual = comparer.IsMatch(x, y);

            Assert.False(actual);
        }

        [Fact]
        public void TableDataCaseSensitiveStringValueComparerIsMatchFalseNullX()
        {
            string x = null;
            string y = "b";

            bool actual = comparer.IsMatch(x, y);

            Assert.False(actual);
        }

        [Fact]
        public void TableDataCaseSensitiveStringValueComparerIsMatchFalseNullY()
        {
            string x = "a";
            string y = null;

            bool actual = comparer.IsMatch(x, y);

            Assert.False(actual);
        }

        [Fact]
        public void TableDataCaseSensitiveStringValueComparerIsMatchTrueBothNull()
        {
            string x = null;
            string y = null;

            bool actual = comparer.IsMatch(x, y);

            Assert.True(actual);
        }
    }
}
