using Xunit;
using System.Collections.Generic;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataMatchEqualColumnComparerTests
    {
        private TableDataMatchEqualColumnComparer comparer;
        private TableData x;
        private TableData y;

        public TableDataMatchEqualColumnComparerTests()
        {
            comparer = new TableDataMatchEqualColumnComparer();
            x = new TableData();
            y = new TableData();
        }

        [Fact]
        public void TableDataMatchEqualColumnComparerIsMatchTrue()
        {
            x.ColumnNames = new List<string> { "1", "2", "3" };
            y.ColumnNames = new List<string> { "1", "2", "3" };
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }

        [Fact]
        public void TableDataMatchEqualColumnComparerIsMatchFalse()
        {
            x.ColumnNames = new List<string> { "1", "2", "3" };
            y.ColumnNames = new List<string> { "1", "2" };
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.False(actual);
        }

        [Fact]
        public void TableDataMatchEqualColumnComparerIsMatchFalse2()
        {
            x.ColumnNames = new List<string> { "1", "2" };
            y.ColumnNames = new List<string> { "1", "2", "3" };
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.False(actual);
        }
    }
}
