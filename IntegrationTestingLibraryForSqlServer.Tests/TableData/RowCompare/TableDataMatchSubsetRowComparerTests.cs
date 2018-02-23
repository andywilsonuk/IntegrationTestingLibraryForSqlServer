using Xunit;
using System.Collections.Generic;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataMatchSubsetRowComparerTests
    {
        private TableDataMatchSubsetRowComparer comparer;
        private TableDataCaseSensitiveStringValueComparer valueComparer;
        private IList<int> indexMappings;

        public TableDataMatchSubsetRowComparerTests()
        {
            indexMappings = new List<int> { 0, 1, 2 };
            comparer = new TableDataMatchSubsetRowComparer();
            valueComparer = new TableDataCaseSensitiveStringValueComparer();
        }

        [Fact]
        public void TableDataMatchSubsetRowComparerIsMatchTrue()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" }, new List<object> { "a", "b", "c" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }

        [Fact]
        public void TableDataMatchSubsetRowComparerIsMatchFalseMissingRow()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.False(actual);
        }

        [Fact]
        public void TableDataMatchSubsetRowComparerIsMatchFalseMismatchedValues()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "b", "a", "c" }, new List<object> { "d", "e", "f" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.False(actual);
        }

        [Fact]
        public void TableDataMatchSubsetComparerIsMatchWithIndexMappingTrue()
        {
            indexMappings = new List<int> { 1, 0, 2 };
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "b", "a", "c" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }

        [Fact]
        public void TableDataMatchSubsetRowComparerIsMatchTrueExtraColumns()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" } };

            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }
    }
}
