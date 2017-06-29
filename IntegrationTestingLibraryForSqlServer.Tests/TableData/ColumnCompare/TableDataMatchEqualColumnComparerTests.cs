using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
            comparer = new TableDataMatchEqualColumnComparer();
            x = new TableData();
            y = new TableData();
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
        public void TableDataMatchEqualColumnComparerIsMatchFalse2()
        {
            x.ColumnNames = new List<string> { "1", "2" };
            y.ColumnNames = new List<string> { "1", "2", "3" };
            comparer.Initialise(x, y);

            bool actual = comparer.IsMatch();

            Assert.IsFalse(actual);
        }
    }
}
