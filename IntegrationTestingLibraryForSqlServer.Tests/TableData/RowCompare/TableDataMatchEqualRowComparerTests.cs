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
            indexMappings = new List<int> { 0, 1, 2 };
            comparer = new TableDataMatchEqualRowComparer();
            valueComparer = new TableDataCaseSensitiveStringValueComparer();
        }

        [TestMethod]
        public void TableDataMatchEqualRowComparerIsMatchTrue()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" }, new List<object> { "a", "b", "c" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataMatchEqualRowComparerIsMatchFalseMismatchedRows()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" } };

            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataMatchEqualRowComparerIsMatchTrueExtraColumns()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" } };

            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.IsTrue(actual);
        }
    }
}
