using Xunit;
using System.Collections.Generic;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataMatchEqualRowComparerTests
    {
        private TableDataMatchEqualRowComparer comparer;
        private TableDataCaseSensitiveStringValueComparer valueComparer;
        private IList<int> indexMappings;
        
        public TableDataMatchEqualRowComparerTests()
        {
            indexMappings = new List<int> { 0, 1, 2 };
            comparer = new TableDataMatchEqualRowComparer();
            valueComparer = new TableDataCaseSensitiveStringValueComparer();
        }

        [Fact]
        public void TableDataMatchEqualRowComparerIsMatchTrue()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" }, new List<object> { "a", "b", "c" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }

        [Fact]
        public void TableDataMatchEqualRowComparerIsMatchFalseMismatchedRows()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" } };

            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.False(actual);
        }

        [Fact]
        public void TableDataMatchEqualRowComparerIsMatchTrueExtraColumns()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" } };

            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }
    }
}
