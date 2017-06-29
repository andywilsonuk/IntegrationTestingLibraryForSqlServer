using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataMatchSubsetRowComparerTests
    {
        private TableDataMatchSubsetRowComparer comparer;
        private TableDataCaseSensitiveStringValueComparer valueComparer;
        private IList<int> indexMappings;

        [TestInitialize]
        public void TestInitialize()
        {
            indexMappings = new List<int> { 0, 1, 2 };
            comparer = new TableDataMatchSubsetRowComparer();
            valueComparer = new TableDataCaseSensitiveStringValueComparer();
        }

        [TestMethod]
        public void TableDataMatchSubsetRowComparerIsMatchTrue()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" }, new List<object> { "a", "b", "c" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataMatchSubsetRowComparerIsMatchFalseMissingRow()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataMatchSubsetRowComparerIsMatchFalseMismatchedValues()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "b", "a", "c" }, new List<object> { "d", "e", "f" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataMatchSubsetComparerIsMatchWithIndexMappingTrue()
        {
            indexMappings = new List<int> { 1, 0, 2 };
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "b", "a", "c" } };
            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataMatchSubsetRowComparerIsMatchTrueExtraColumns()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" } };

            comparer.Initialise(rowsX, rowsY, indexMappings, valueComparer);

            bool actual = comparer.IsMatch();

            Assert.IsTrue(actual);
        }
    }
}
