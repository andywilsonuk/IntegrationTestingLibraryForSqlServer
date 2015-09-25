using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataTests
    {
        TableData tableData;

        [TestInitialize]
        public void TestInitialize()
        {
            this.tableData = new TableData();
            this.tableData.ColumnNames = new List<string> { "a", "b" };
            this.tableData.Rows.Add(new List<object> { "1", "2" });
        }

        [TestMethod]
        public void TableDataConstructor()
        {
            Assert.IsNotNull(this.tableData.ColumnNames);
            Assert.IsNotNull(this.tableData.Rows);
        }

        [TestMethod]
        public void TableDataIsMatchSpecificComparerFromEnum()
        {
            var other = new TableData();
            other.ColumnNames = this.tableData.ColumnNames;
            other.Rows = this.tableData.Rows;

            bool actual = this.tableData.IsMatch(other, TableDataComparers.OrdinalRowOrdinalColumn);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataIsMatchCustomComparer()
        {
            var other = new TableData();
            other.ColumnNames = this.tableData.ColumnNames;
            other.Rows = this.tableData.Rows;
            var strategy = new TableDataComparerStrategyFactory().Comparer(TableDataComparers.OrdinalRowOrdinalColumn);

            bool actual = this.tableData.IsMatch(other, strategy);

            Assert.IsTrue(actual);
        }
    }
}
