using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataOrdinalColumnComparerTests
    {
        private TableDataOrdinalColumnComparer comparer;
        private TableData x;
        private TableData y;

        public TableDataOrdinalColumnComparerTests()
        {
            comparer = new TableDataOrdinalColumnComparer();
            x = new TableData();
            y = new TableData();
        }

        [Fact]
        public void TableDataOrdinalColumnComparerColumnMappingsNumberedCorrectly()
        {
            var expected = new List<int> { 0, 1, 2 };
            x.Rows.Add(new[] { "a", "b", "c" });
            y.Rows.Add(new[] { "a", "b", "c" });
            comparer.Initialise(x, y);

            var actual = comparer.ColumnMappings;

            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void TableDataOrdinalColumnComparerIsMatchTrue()
        {
            x.Rows.Add(new[] { "a", "b", "c" });
            y.Rows.Add(new[] { "a", "b", "c" });
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }

        [Fact]
        public void TableDataOrdinalColumnComparerIsMatchFalseMismatchedRows()
        {
            x.Rows.Add(new[] { "a", "b", "c" });
            y.Rows.Add(new[] { "a", "b" });
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.False(actual);
        }

        [Fact]
        public void TableDataOrdinalColumnComparerIsMatchFalseNoYRows()
        {
            x.Rows.Add(new[] { "a" });
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.False(actual);
        }

        [Fact]
        public void TableDataOrdinalColumnComparerInitialiseInvalidRows()
        {
            y.Rows.Add(new[] { "a", "b", "c" });
            Assert.Throws<ArgumentException>(() => comparer.Initialise(x, y));
        }

        [Fact]
        public void TableDataOrdinalColumnComparerInitialiseZeroColumnsInFirstRow()
        {
            x.Rows.Add(new object[] { });
            Assert.Throws<ArgumentException>(() => comparer.Initialise(x, y));
        }

        [Fact]
        public void TableDataOrdinalColumnComparerIsMatchZeroRows()
        {
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }

        [Fact]
        public void TableDataOrdinalColumnComparerColumnMappingsZeroRows()
        {
            comparer.Initialise(x, y);

            IList<int> actual = comparer.ColumnMappings;

            Assert.Equal(0, actual.Count);
        }
    }
}
