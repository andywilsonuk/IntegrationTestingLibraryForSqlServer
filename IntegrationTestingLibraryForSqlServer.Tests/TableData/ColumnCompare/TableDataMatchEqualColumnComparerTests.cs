using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataMatchEqualColumnComparerTests
    {
        private TableDataMatchEqualColumnComparer comparer;
        private TableData x;
        private TableData y;

        [TestInitialize]
        public void TestInitialize()
        {
            this.comparer = new TableDataMatchEqualColumnComparer();
            this.x = new TableData();
            this.y = new TableData();
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
        public void TableDataMatchEqualColumnComparerIsMatchFalse2()
        {
            x.ColumnNames = new List<string> { "1", "2" };
            y.ColumnNames = new List<string> { "1", "2", "3" };
            this.comparer.Initialise(x, y);

            bool actual = this.comparer.IsMatch();

            Assert.IsFalse(actual);
        }
    }
}
