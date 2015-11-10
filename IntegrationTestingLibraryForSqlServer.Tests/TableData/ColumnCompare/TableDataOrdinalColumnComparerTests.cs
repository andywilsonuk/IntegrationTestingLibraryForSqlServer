using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataOrdinalColumnComparerTests
    {
        private TableDataOrdinalColumnComparer comparer;
        private TableData x;
        private TableData y;

        [TestInitialize]
        public void TestInitialize()
        {
            this.comparer = new TableDataOrdinalColumnComparer();
            this.x = new TableData();
            this.y = new TableData();
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerColumnMappingsNumberedCorrectly()
        {
            var expected = new List<int> { 0, 1, 2 };
            x.Rows.Add(new[] { "a", "b", "c" });
            y.Rows.Add(new[] { "a", "b", "c" });
            this.comparer.Initialise(x, y);

            var actual = this.comparer.ColumnMappings;

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerIsMatchTrue()
        {
            x.Rows.Add(new[] { "a", "b", "c" });
            y.Rows.Add(new[] { "a", "b", "c" });
            this.comparer.Initialise(x, y);

            bool actual = this.comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerIsMatchFalseMismatchedRows()
        {
            x.Rows.Add(new[] { "a", "b", "c" });
            y.Rows.Add(new[] { "a", "b" });
            this.comparer.Initialise(x, y);

            bool actual = this.comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerIsMatchFalseNoYRows()
        {
            x.Rows.Add(new[] { "a" });
            this.comparer.Initialise(x, y);

            bool actual = this.comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TableDataOrdinalColumnComparerInitialiseInvalidRows()
        {
            y.Rows.Add(new[] { "a", "b", "c" });
            this.comparer.Initialise(x, y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TableDataOrdinalColumnComparerInitialiseZeroColumnsInFirstRow()
        {
            x.Rows.Add(new object[] { });
            this.comparer.Initialise(x, y);
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerIsMatchZeroRows()
        {
            this.comparer.Initialise(x, y);

            bool actual = this.comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerColumnMappingsZeroRows()
        {
            this.comparer.Initialise(x, y);

            IList<int> actual = this.comparer.ColumnMappings;

            Assert.AreEqual(0, actual.Count);
        }
    }
}
