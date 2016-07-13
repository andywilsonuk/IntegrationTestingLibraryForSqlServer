using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataMatchSubsetColumnComparerTests
    {
        private TableDataMatchSubsetColumnComparer comparer;
        private TableData x;
        private TableData y;

        [TestInitialize]
        public void TestInitialize()
        {
            comparer = new TableDataMatchSubsetColumnComparer();
            x = new TableData();
            x.ColumnNames = GetDefaultColumnNames();
            y = new TableData();
            y.ColumnNames = GetDefaultColumnNames();
        }

        private IList<string> GetDefaultColumnNames()
        {
            return new List<string> { "1", "2", "3" };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataMatchEqualColumnComparerInitialiseFailMissingXColumnNames()
        {
            x.ColumnNames = null;

            comparer.Initialise(x, y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataMatchEqualColumnComparerInitialiseFailMissingYColumnNames()
        {
            y.ColumnNames = null;

            comparer.Initialise(x, y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataMatchEqualColumnComparerInitialiseFailEmptyXColumnNames()
        {
            x.ColumnNames = new List<string>();

            comparer.Initialise(x, y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataMatchEqualColumnComparerInitialiseFailEmptyYColumnNames()
        {
            y.ColumnNames = new List<string>();

            comparer.Initialise(x, y);
        }

        [TestMethod]
        public void TableDataMatchEqualColumnComparerColumnMappingsNumberedCorrectly()
        {
            var expected = new List<int> { 1, 0, 2 };
            x.Rows.Add(new[] { "a", "b", "c" });
            y.ColumnNames = new List<string> { "2", "1", "3" };
            y.Rows.Add(new[] { "b", "a", "c" });
            comparer.Initialise(x, y);

            var actual = comparer.ColumnMappings;

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void TableDataMatchEqualColumnComparerIsMatchTrue()
        {
            x.ColumnNames = new List<string> { "1", "2", "3" };
            y.ColumnNames = new List<string> { "1", "2", "3" };
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataMatchEqualColumnComparerIsMatchFalse()
        {
            x.ColumnNames = new List<string> { "1", "2", "3" };
            y.ColumnNames = new List<string> { "1", "2" };
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataMatchEqualColumnComparerIsMatchTrue2()
        {
            x.ColumnNames = new List<string> { "1", "2" };
            y.ColumnNames = new List<string> { "1", "2", "3" };
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.IsTrue(actual);
        }
    }
}
