using System;
using System.Collections.Generic;
using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataTests
    {
        TableData tableData;

        public TableDataTests()
        {
            tableData = new TableData
            {
                ColumnNames = new List<string> { "a", "b" }
            };
            tableData.Rows.Add(new List<object> { "1", "2" });
        }

        [Fact]
        public void TableDataConstructor()
        {
            Assert.NotNull(tableData.ColumnNames);
            Assert.NotNull(tableData.Rows);
        }

        [Fact]
        public void TableDataIsMatchSpecificComparerFromEnum()
        {
            var other = new TableData
            {
                ColumnNames = tableData.ColumnNames,
                Rows = tableData.Rows
            };

            bool actual = tableData.IsMatch(other, TableDataComparers.OrdinalRowOrdinalColumn);

            Assert.True(actual);
        }

        [Fact]
        public void TableDataIsMatchCustomComparer()
        {
            var other = new TableData
            {
                ColumnNames = tableData.ColumnNames,
                Rows = tableData.Rows
            };
            var strategy = new TableDataComparerStrategyFactory().Comparer(TableDataComparers.OrdinalRowOrdinalColumn);

            bool actual = tableData.IsMatch(other, strategy);

            Assert.True(actual);
        }

        [Fact]
        public void TableDataIsMatchNullCustomComparerThrows()
        {
            var other = new TableData();

            Assert.Throws<ArgumentNullException>(() => tableData.IsMatch(other, (TableDataComparer)null));
        }

        [Fact]
        public void TableDataToString()
        {
            string expected = @"Column names: a, b. 
1, 2. 
";

            string actual = tableData.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TableDataToStringNoColumnNames()
        {
            tableData.ColumnNames = null;
            string expected = @"Column names: . 
1, 2. 
";

            string actual = tableData.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TableDataVerifyMatchSpecificComparerFromEnum()
        {
            var other = new TableData
            {
                ColumnNames = tableData.ColumnNames,
                Rows = tableData.Rows
            };

            tableData.VerifyMatch(other, TableDataComparers.OrdinalRowOrdinalColumn);
        }

        [Fact]
        public void TableDataVerifyMatchSpecificComparerFromEnumThrows()
        {
            var other = new TableData
            {
                ColumnNames = tableData.ColumnNames
            };

            Assert.Throws<EquivalenceException>(() => tableData.VerifyMatch(other, TableDataComparers.OrdinalRowOrdinalColumn));
        }

        [Fact]
        public void TableDataIsMatchCustomComparerThrows()
        {
            var other = new TableData
            {
                ColumnNames = tableData.ColumnNames
            };
            var strategy = new TableDataComparerStrategyFactory().Comparer(TableDataComparers.OrdinalRowOrdinalColumn);

            Assert.Throws<EquivalenceException>(() => tableData.VerifyMatch(other, strategy));
        }
    }
}
