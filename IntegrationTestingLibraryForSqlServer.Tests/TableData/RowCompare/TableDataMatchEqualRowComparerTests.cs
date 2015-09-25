using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataMatchEqualRowComparerTests
    {
        private TableDataMatchEqualRowComparer comparer;
        private TableDataCaseSensitiveStringValueComparer valueComparer;
        private IList<int> indexMappings;
        
        [TestInitialize]
        public void TestInitialize()
        {
            this.indexMappings = new List<int> { 0, 1, 2 };
            this.comparer = new TableDataMatchEqualRowComparer();
            this.valueComparer = new TableDataCaseSensitiveStringValueComparer();
        }

        [TestMethod]
        public void TableDataMatchSubsetRowComparerIsMatchTrue()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" }, new List<object> { "a", "b", "c" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataMatchSubsetRowComparerIsMatchFalseMismatchedRows()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" } };

            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsFalse(actual);
        }
    }
}
