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
            this.columnComparer = new Mock<TableDataColumnComparer>();
            this.rowComparer = new Mock<TableDataRowComparer>();
            this.valueComparer = new Mock<TableDataValueComparer>();
            this.comparer = new TableDataCompositeComparer(this.columnComparer.Object, this.rowComparer.Object, this.valueComparer.Object);
            this.x = new TableData();
            this.y = new TableData();
        }

        [TestMethod]
        public void TableDataCompositeComparerIsMatchTrue()
        {
            this.columnComparer.Setup(x => x.IsMatch()).Returns(true);
            this.rowComparer.Setup(x => x.IsMatch()).Returns(true);

            bool actual = this.comparer.IsMatch(this.x, this.y);

            Assert.IsTrue(actual);
            this.columnComparer.Verify(x => x.Initialise(this.x, this.y), Times.Once);
            this.columnComparer.Verify(x => x.IsMatch(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataCompositeComparerIsMatchFalseNullRowsX()
        {
            this.x.Rows = null;

            this.comparer.IsMatch(this.x, this.y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableDataCompositeComparerIsMatchFalseNullRowsY()
        {
            this.y.Rows = null;

            this.comparer.IsMatch(this.x, this.y);
        }

        [TestMethod]
        public void TableDataCompositeComparerIsMatchFalseMismatchedColumns()
        {
            this.columnComparer.Setup(x => x.IsMatch()).Returns(false);

            bool actual = this.comparer.IsMatch(this.x, this.y);

            Assert.IsFalse(actual);
            this.columnComparer.Verify(x => x.Initialise(this.x, this.y), Times.Once);
            this.columnComparer.Verify(x => x.IsMatch(), Times.Once);
        }

        [TestMethod]
        public void TableDataCompositeComparerIsMatchFalse()
        {
            this.columnComparer.Setup(x => x.IsMatch()).Returns(true);
            this.rowComparer.Setup(x => x.IsMatch()).Returns(false);

            bool actual = this.comparer.IsMatch(this.x, this.y);

            Assert.IsFalse(actual);
        }
    }
}
