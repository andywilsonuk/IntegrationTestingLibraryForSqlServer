using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataMatchSubsetColumnComparerTests
    {
        private TableDataMatchSubsetColumnComparer comparer;
        private TableData x;
        private TableData y;

        public TableDataMatchSubsetColumnComparerTests()
        {
            comparer = new TableDataMatchSubsetColumnComparer();
            x = new TableData
            {
                ColumnNames = GetDefaultColumnNames()
            };
            y = new TableData
            {
                ColumnNames = GetDefaultColumnNames()
            };
        }

        private IList<string> GetDefaultColumnNames()
        {
            return new List<string> { "1", "2", "3" };
        }

        [Fact]
        public void TableDataMatchEqualColumnComparerInitialiseFailMissingXColumnNames()
        {
            x.ColumnNames = null;

            Assert.Throws<ArgumentNullException>(() => comparer.Initialise(x, y));
        }

        [Fact]
        public void TableDataMatchEqualColumnComparerInitialiseFailMissingYColumnNames()
        {
            y.ColumnNames = null;

            Assert.Throws<ArgumentNullException>(() => comparer.Initialise(x, y));
        }

        [Fact]
        public void TableDataMatchEqualColumnComparerInitialiseFailEmptyXColumnNames()
        {
            x.ColumnNames = new List<string>();

            Assert.Throws<ArgumentNullException>(() => comparer.Initialise(x, y));
        }

        [Fact]
        public void TableDataMatchEqualColumnComparerInitialiseFailEmptyYColumnNames()
        {
            y.ColumnNames = new List<string>();

            Assert.Throws<ArgumentNullException>(() => comparer.Initialise(x, y));
        }

        [Fact]
        public void TableDataMatchEqualColumnComparerColumnMappingsNumberedCorrectly()
        {
            var expected = new List<int> { 1, 0, 2 };
            x.Rows.Add(new[] { "a", "b", "c" });
            y.ColumnNames = new List<string> { "2", "1", "3" };
            y.Rows.Add(new[] { "b", "a", "c" });
            comparer.Initialise(x, y);

            var actual = comparer.ColumnMappings;

            Assert.True(expected.SequenceEqual(actual));
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
        public void TableDataMatchEqualColumnComparerIsMatchTrue2()
        {
            x.ColumnNames = new List<string> { "1", "2" };
            y.ColumnNames = new List<string> { "1", "2", "3" };
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.True(actual);
        }
    }
}
