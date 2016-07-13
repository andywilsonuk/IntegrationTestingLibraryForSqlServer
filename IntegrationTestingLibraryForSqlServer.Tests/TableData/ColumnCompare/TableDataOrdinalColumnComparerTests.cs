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
            comparer = new TableDataOrdinalColumnComparer();
            x = new TableData();
            y = new TableData();
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerColumnMappingsNumberedCorrectly()
        {
            var expected = new List<int> { 0, 1, 2 };
            x.Rows.Add(new[] { "a", "b", "c" });
            y.Rows.Add(new[] { "a", "b", "c" });
            comparer.Initialise(x, y);

            var actual = comparer.ColumnMappings;

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerIsMatchTrue()
        {
            x.Rows.Add(new[] { "a", "b", "c" });
            y.Rows.Add(new[] { "a", "b", "c" });
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerIsMatchFalseMismatchedRows()
        {
            x.Rows.Add(new[] { "a", "b", "c" });
            y.Rows.Add(new[] { "a", "b" });
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerIsMatchFalseNoYRows()
        {
            x.Rows.Add(new[] { "a" });
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TableDataOrdinalColumnComparerInitialiseInvalidRows()
        {
            y.Rows.Add(new[] { "a", "b", "c" });
            comparer.Initialise(x, y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TableDataOrdinalColumnComparerInitialiseZeroColumnsInFirstRow()
        {
            x.Rows.Add(new object[] { });
            comparer.Initialise(x, y);
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerIsMatchZeroRows()
        {
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataOrdinalColumnComparerColumnMappingsZeroRows()
        {
            comparer.Initialise(x, y);

            IList<int> actual = comparer.ColumnMappings;

            Assert.AreEqual(0, actual.Count);
        }
    }
}
