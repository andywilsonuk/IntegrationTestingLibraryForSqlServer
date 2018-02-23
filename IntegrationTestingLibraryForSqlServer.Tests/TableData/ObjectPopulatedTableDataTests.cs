using System;
using Xunit;
using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class ObjectPopulatedTableDataTests
    {
        [Fact]
        public void PopulateFromObject()
        {
            var source = new List<Tuple<string, int>>
            {
                new Tuple<string, int>("a", 1),
                new Tuple<string, int>("b", 2),
            };

            var tableData = new ObjectPopulatedTableData<Tuple<string, int>>(source);

            Assert.Equal(2, tableData.ColumnNames.Count);
            Assert.True(tableData.ColumnNames.Contains("Item1"));
            Assert.True(tableData.ColumnNames.Contains("Item2"));
            Assert.Equal("a", tableData.Rows[0][0]);
            Assert.Equal(1, tableData.Rows[0][1]);
            Assert.Equal("b", tableData.Rows[1][0]);
            Assert.Equal(2, tableData.Rows[1][1]);
        }
    }
}
