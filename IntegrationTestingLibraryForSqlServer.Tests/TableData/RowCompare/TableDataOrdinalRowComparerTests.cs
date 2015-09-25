using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataOrdinalRowComparerTests
    {
        private TableDataOrdinalRowComparer comparer;
        private TableDataCaseSensitiveStringValueComparer valueComparer;
        private IList<int> indexMappings;

        [TestInitialize]
        public void TestInitialize()
        {
            this.indexMappings = new List<int> { 0, 1, 2 };
            this.comparer = new TableDataOrdinalRowComparer();
            this.valueComparer = new TableDataCaseSensitiveStringValueComparer();
        }

        [TestMethod]
        public void TableDataOrdinalRowComparerIsMatchTrue()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataOrdinalRowComparerIsMatchMismatchedRows()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataOrdinalRowComparerIsMatchFalse()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "b", "a", "c" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TableDataOrdinalRowComparerIsMatchWithIndexMappingTrue()
        {
            this.indexMappings = new List<int> { 1, 0, 2 };
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "b", "a", "c" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TableDataOrdinalRowComparerIsMatchMismatchedRowsY()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" }, new List<object> { "d", "e", "f" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);

            bool actual = this.comparer.IsMatch();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TableDataOrdinalRowComparerIsMatchMismatchedColumnValuesX()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TableDataOrdinalRowComparerIsMatchMismatchedColumnsValueY()
        {
            var rowsX = new List<IList<object>> { new List<object> { "a", "b", "c" } };
            var rowsY = new List<IList<object>> { new List<object> { "a", "b" } };
            this.comparer.Initialise(rowsX, rowsY, this.indexMappings, this.valueComparer);
        }
    }
}
