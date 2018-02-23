using System;
using Xunit;
using System.Collections.Generic;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataOrdinalRowComparerTests
    {
        private TableDataOrdinalRowComparer comparer;
        private TableDataCaseSensitiveStringValueComparer valueComparer;
        private IList<int> indexMappings;

        public TableDataOrdinalRowComparerTests()
        {
            indexMappings = new List<int> { 0, 1, 2 };
            comparer = new TableDataOrdinalRowComparer();
            valueComparer = new TableDataCaseSensitiveStringValueComparer();
        }

        [Fact]
        public void TableDataOrdinalRowComparerIsMatchTrue()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }

        [Fact]
        public void TableDataOrdinalRowComparerIsMatchMismatchedRows()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.False(actual);
        }

        [Fact]
        public void TableDataOrdinalRowComparerIsMatchFalse()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "b", "a", "c" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.False(actual);
        }

        [Fact]
        public void TableDataOrdinalRowComparerIsMatchWithIndexMappingTrue()
        {
            indexMappings = new List<int> { 1, 0, 2 };
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "b", "a", "c" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }

        [Fact]
        public void TableDataOrdinalRowComparerIsMatchMismatchedRowsY()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.False(actual);
        }

        [Fact]
        public void TableDataOrdinalRowComparerIsMatchMismatchedColumnValuesX()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            Assert.Throws<ArgumentException>(() => comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer));
        }

        [Fact]
        public void TableDataOrdinalRowComparerIsMatchMismatchedColumnsValueY()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b" } };
            Assert.Throws<ArgumentException>(() => comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer));
        }
    }
}
