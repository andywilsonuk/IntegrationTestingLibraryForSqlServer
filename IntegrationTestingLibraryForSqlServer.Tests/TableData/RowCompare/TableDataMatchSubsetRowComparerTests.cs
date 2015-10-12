using System;
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
            this.indexMappings = new List<int> { 0, 1, 2 };
            this.comparer = new TableDataMatchSubsetRowComparer();
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
        public void TableDataMatchSubsetRowComparerIsMatchFalseMissingRow()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "d", "e", "f" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataMatchSubsetRowComparerIsMatchFalseMismatchedValues()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "b", "a", "c" }, new List<object> { "d", "e", "f" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataMatchSubsetComparerIsMatchWithIndexMappingTrue()
        {
            this.indexMappings = new List<int> { 1, 0, 2 };
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "b", "a", "c" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataMatchSubsetRowComparerIsMatchTrueExtraColumns()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" } };

            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsTrue(actual);
        }
    }
}
