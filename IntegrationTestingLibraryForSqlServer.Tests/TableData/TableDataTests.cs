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
            tableData = new TableData();
            tableData.ColumnNames = new List<string> { "a", "b" };
            tableData.Rows.Add(new List<object> { "1", "2" });
        }

        [TestMethod]
        public void TableDataConstructor()
        {
            Assert.IsNotNull(tableData.ColumnNames);
            Assert.IsNotNull(tableData.Rows);
        }

        [TestMethod]
        public void TableDataIsMatchSpecificComparerFromEnum()
        {
            var other = new TableData();
            other.ColumnNames = tableData.ColumnNames;
            other.Rows = tableData.Rows;

            bool actual = tableData.IsMatch(other, TableDataComparers.OrdinalRowOrdinalColumn);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataIsMatchCustomComparer()
        {
            var other = new TableData();
            other.ColumnNames = tableData.ColumnNames;
            other.Rows = tableData.Rows;
            var strategy = new TableDataComparerStrategyFactory().Comparer(TableDataComparers.OrdinalRowOrdinalColumn);

            bool actual = tableData.IsMatch(other, strategy);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataIsMatchNullCustomComparerThrows()
        {
            var other = new TableData();

            tableData.IsMatch(other, (TableDataComparer)null);
        }

        [TestMethod]
        public void TableDataToString()
        {
            string expected = @"Column names: a, b. 
1, 2. 
";

            string actual = tableData.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDataToStringNoColumnNames()
        {
            tableData.ColumnNames = null;
            string expected = @"Column names: . 
1, 2. 
";

            string actual = tableData.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TableDataVerifyMatchSpecificComparerFromEnum()
        {
            var other = new TableData();
            other.ColumnNames = tableData.ColumnNames;
            other.Rows = tableData.Rows;

            tableData.VerifyMatch(other, TableDataComparers.OrdinalRowOrdinalColumn);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDataVerifyMatchSpecificComparerFromEnumThrows()
        {
            var other = new TableData();
            other.ColumnNames = tableData.ColumnNames;

            tableData.VerifyMatch(other, TableDataComparers.OrdinalRowOrdinalColumn);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void TableDataIsMatchCustomComparerThrows()
        {
            var other = new TableData();
            other.ColumnNames = tableData.ColumnNames;
            var strategy = new TableDataComparerStrategyFactory().Comparer(TableDataComparers.OrdinalRowOrdinalColumn);

            tableData.VerifyMatch(other, strategy);
        }
    }
}
