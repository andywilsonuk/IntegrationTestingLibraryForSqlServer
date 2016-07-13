using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;
using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataCompositeComparerTests
    {
        private Mock<TableDataColumnComparer> columnComparer;
        private Mock<TableDataRowComparer> rowComparer;
        private Mock<TableDataValueComparer> valueComparer;
        private TableDataCompositeComparer comparer;
        private TableData x;
        private TableData y;

        [TestInitialize]
        public void TestInitialize()
        {
            columnComparer = new Mock<TableDataColumnComparer>();
            rowComparer = new Mock<TableDataRowComparer>();
            valueComparer = new Mock<TableDataValueComparer>();
            comparer = new TableDataCompositeComparer(columnComparer.Object, rowComparer.Object, valueComparer.Object);
            x = new TableData();
            y = new TableData();
        }

        [TestMethod]
        public void TableDataCompositeComparerIsMatchTrue()
        {
            columnComparer.Setup(x => x.IsMatch()).Returns(true);
            rowComparer.Setup(x => x.IsMatch()).Returns(true);

            bool actual = comparer.IsMatch(this.x, y);

            Assert.IsTrue(actual);
            columnComparer.Verify(x => x.Initialise(this.x, y), Times.Once);
            columnComparer.Verify(x => x.IsMatch(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataCompositeComparerIsMatchFalseNullRowsX()
        {
            x.Rows = null;

            comparer.IsMatch(x, y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataCompositeComparerIsMatchFalseNullRowsY()
        {
            y.Rows = null;

            comparer.IsMatch(x, y);
        }

        [TestMethod]
        public void TableDataCompositeComparerIsMatchFalseMismatchedColumns()
        {
            columnComparer.Setup(x => x.IsMatch()).Returns(false);

            bool actual = comparer.IsMatch(this.x, y);

            Assert.IsFalse(actual);
            columnComparer.Verify(x => x.Initialise(this.x, y), Times.Once);
            columnComparer.Verify(x => x.IsMatch(), Times.Once);
        }

        [TestMethod]
        public void TableDataCompositeComparerIsMatchFalse()
        {
            columnComparer.Setup(x => x.IsMatch()).Returns(true);
            rowComparer.Setup(x => x.IsMatch()).Returns(false);

            bool actual = comparer.IsMatch(this.x, y);

            Assert.IsFalse(actual);
        }
    }
}
