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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataIsMatchNullCustomComparerThrows()
        {
            var other = new TableData();

            this.tableData.IsMatch(other, (TableDataComparer)null);
        }

        [TestMethod]
        public void TableDataToString()
        {
            string expected = @"Column names: a, b
1, 2
";

            string actual = this.tableData.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDataToStringNoColumnNames()
        {
            this.tableData.ColumnNames = null;
            string expected = @"Column names: 
1, 2
";

            string actual = this.tableData.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDataVerifyMatchSpecificComparerFromEnum()
        {
            var other = new TableData();
            other.ColumnNames = this.tableData.ColumnNames;
            other.Rows = this.tableData.Rows;

            this.tableData.VerifyMatch(other, TableDataComparers.OrdinalRowOrdinalColumn);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDataVerifyMatchSpecificComparerFromEnumThrows()
        {
            var other = new TableData();
            other.ColumnNames = this.tableData.ColumnNames;

            this.tableData.VerifyMatch(other, TableDataComparers.OrdinalRowOrdinalColumn);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDataIsMatchCustomComparerThrows()
        {
            var other = new TableData();
            other.ColumnNames = this.tableData.ColumnNames;
            var strategy = new TableDataComparerStrategyFactory().Comparer(TableDataComparers.OrdinalRowOrdinalColumn);

            this.tableData.VerifyMatch(other, strategy);
        }
    }
}
