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
            this.comparer = new TableDataMatchSubsetColumnComparer();
            this.x = new TableData();
            this.x.ColumnNames = this.GetDefaultColumnNames();
            this.y = new TableData();
            this.y.ColumnNames = this.GetDefaultColumnNames();
        }

        private IList<string> GetDefaultColumnNames()
        {
            return new List<string> { "1", "2", "3" };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataMatchEqualColumnComparerInitialiseFailMissingXColumnNames()
        {
            this.x.ColumnNames = null;

            this.comparer.Initialise(this.x, this.y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataMatchEqualColumnComparerInitialiseFailMissingYColumnNames()
        {
            this.y.ColumnNames = null;

            this.comparer.Initialise(this.x, this.y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataMatchEqualColumnComparerInitialiseFailEmptyXColumnNames()
        {
            this.x.ColumnNames = new List<string>();

            this.comparer.Initialise(this.x, this.y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataMatchEqualColumnComparerInitialiseFailEmptyYColumnNames()
        {
            this.y.ColumnNames = new List<string>();

            this.comparer.Initialise(this.x, this.y);
        }

        [TestMethod]
        public void TableDataMatchEqualColumnComparerColumnMappingsNumberedCorrectly()
        {
            var expected = new List<int> { 1, 0, 2 };
            x.Rows.Add(new[] { "a", "b", "c" });
            y.ColumnNames = new List<string> { "2", "1", "3" };
            y.Rows.Add(new[] { "b", "a", "c" });
            this.comparer.Initialise(x, y);

            var actual = this.comparer.ColumnMappings;

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void TableDataMatchEqualColumnComparerIsMatchTrue()
        {
            x.ColumnNames = new List<string> { "1", "2", "3" };
            y.ColumnNames = new List<string> { "1", "2", "3" };
            this.comparer.Initialise(x, y);

            bool actual = this.comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataMatchEqualColumnComparerIsMatchFalse()
        {
            x.ColumnNames = new List<string> { "1", "2", "3" };
            y.ColumnNames = new List<string> { "1", "2" };
            this.comparer.Initialise(x, y);

            bool actual = this.comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataMatchEqualColumnComparerIsMatchTrue2()
        {
            x.ColumnNames = new List<string> { "1", "2" };
            y.ColumnNames = new List<string> { "1", "2", "3" };
            this.comparer.Initialise(x, y);

            bool actual = this.comparer.IsMatch();

            Assert.IsTrue(actual);
        }
    }
}
