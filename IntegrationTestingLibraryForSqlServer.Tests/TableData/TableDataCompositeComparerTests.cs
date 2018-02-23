using System;
using Xunit;
using Moq;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataCompositeComparerTests
    {
        private Mock<TableDataColumnComparer> columnComparer;
        private Mock<TableDataRowComparer> rowComparer;
        private Mock<TableDataValueComparer> valueComparer;
        private TableDataCompositeComparer comparer;
        private TableData x;
        private TableData y;

        public TableDataCompositeComparerTests()
        {
            columnComparer = new Mock<TableDataColumnComparer>();
            rowComparer = new Mock<TableDataRowComparer>();
            valueComparer = new Mock<TableDataValueComparer>();
            comparer = new TableDataCompositeComparer(columnComparer.Object, rowComparer.Object, valueComparer.Object);
            x = new TableData();
            y = new TableData();
        }

        [Fact]
        public void TableDataCompositeComparerIsMatchTrue()
        {
            columnComparer.Setup(x => x.IsMatch()).Returns(true);
            rowComparer.Setup(x => x.IsMatch()).Returns(true);

            bool actual = comparer.IsMatch(this.x, y);

            Assert.True(actual);
            columnComparer.Verify(x => x.Initialise(this.x, y), Times.Once);
            columnComparer.Verify(x => x.IsMatch(), Times.Once);
        }

        [Fact]
        public void TableDataCompositeComparerIsMatchFalseNullRowsX()
        {
            x.Rows = null;

            Assert.Throws<ArgumentNullException>(() => comparer.IsMatch(x, y));
        }

        [Fact]
        public void TableDataCompositeComparerIsMatchFalseNullRowsY()
        {
            y.Rows = null;

            Assert.Throws<ArgumentNullException>(() => comparer.IsMatch(x, y));
        }

        [Fact]
        public void TableDataCompositeComparerIsMatchFalseMismatchedColumns()
        {
            columnComparer.Setup(x => x.IsMatch()).Returns(false);

            bool actual = comparer.IsMatch(this.x, y);

            Assert.False(actual);
            columnComparer.Verify(x => x.Initialise(this.x, y), Times.Once);
            columnComparer.Verify(x => x.IsMatch(), Times.Once);
        }

        [Fact]
        public void TableDataCompositeComparerIsMatchFalse()
        {
            columnComparer.Setup(x => x.IsMatch()).Returns(true);
            rowComparer.Setup(x => x.IsMatch()).Returns(false);

            bool actual = comparer.IsMatch(this.x, y);

            Assert.False(actual);
        }
    }
}
