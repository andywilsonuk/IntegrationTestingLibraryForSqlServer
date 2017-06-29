using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Moq;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DataReaderPopulatedTableDataTests
    {
        private Mock<IDataReader> reader;

        [TestMethod]
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

            Assert.AreEqual(2, tableData.ColumnNames.Count);
            Assert.AreEqual("a", tableData.ColumnNames[0]);
            Assert.AreEqual("b", tableData.ColumnNames[1]);
            Assert.AreEqual(2, tableData.Rows.Count);
            Assert.AreEqual(1, tableData.Rows[0][0]);
            Assert.AreEqual(2, tableData.Rows[0][1]);
            Assert.AreEqual(3, tableData.Rows[1][0]);
            Assert.AreEqual(4, tableData.Rows[1][1]);
        }
    }
}
