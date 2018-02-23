using Xunit;
using System.Data;
using Moq;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class DataReaderPopulatedTableDataTests
    {
        private Mock<IDataReader> reader;

        [Fact]
        public void DataReaderPopulatedTableData2x2Reader()
        {
            reader = new Mock<IDataReader>();
            reader.Setup(x => x.FieldCount).Returns(2);
            reader.Setup(x => x.GetName(0)).Returns("a");
            reader.Setup(x => x.GetName(1)).Returns("b");
            reader.SetupSequence(x => x.Read()).Returns(true).Returns(true).Returns(false);
            reader.SetupSequence(x => x.GetValue(0)).Returns(1).Returns(3);
            reader.SetupSequence(x => x.GetValue(1)).Returns(2).Returns(4);

            TableData tableData = new DataReaderPopulatedTableData(reader.Object);

            Assert.Equal(2, tableData.ColumnNames.Count);
            Assert.Equal("a", tableData.ColumnNames[0]);
            Assert.Equal("b", tableData.ColumnNames[1]);
            Assert.Equal(2, tableData.Rows.Count);
            Assert.Equal(1, tableData.Rows[0][0]);
            Assert.Equal(2, tableData.Rows[0][1]);
            Assert.Equal(3, tableData.Rows[1][0]);
            Assert.Equal(4, tableData.Rows[1][1]);
        }
    }
}
