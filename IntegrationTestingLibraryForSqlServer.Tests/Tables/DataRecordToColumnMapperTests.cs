using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Moq;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DataRecordToColumnMapperTests
    {
        private DataRecordToColumnMapper mapper = new DataRecordToColumnMapper();
        private Mock<IDataRecord> mockDataRecord;
        private ColumnDefinition expected;
        
        [TestInitialize]
        public void TestInitialize()
        {
            mockDataRecord = new Mock<IDataRecord>();
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.Name)).Returns("r1");
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("Decimal");
            mockDataRecord.Setup(x => x.GetInt16(DataRecordToColumnMapper.Columns.Size)).Returns(10);
            mockDataRecord.Setup(x => x.GetByte(DataRecordToColumnMapper.Columns.Precision)).Returns(5);
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsNullable)).Returns(false);
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsIdentity)).Returns(false);

            expected = new ColumnDefinition
            {
                Name = "r1",
                DataType = SqlDbType.Decimal,
                Size = 10,
                Precision = 5,
                AllowNulls = false,
            };
        }

        [TestMethod]
        public void DataRecordToColumnDefinition()
        {
            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DataRecordToColumnDefinitionNullable()
        {
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsNullable)).Returns(true);
            expected.AllowNulls = true;

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DataRecordToColumnPrecisionNonDecimal()
        {
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("Int");
            mockDataRecord.Setup(x => x.GetByte(DataRecordToColumnMapper.Columns.Precision)).Returns(5);
            expected.DataType = SqlDbType.Int;
            expected.Size = null;
            expected.Precision = null;

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DataRecordToColumnIdentity()
        {
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsIdentity)).Returns(true);
            mockDataRecord.Setup(x => x.GetDecimal(DataRecordToColumnMapper.Columns.IdentitySeed)).Returns(5);

            expected.IdentitySeed = 5;

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DataRecordToColumnSizeVarChar()
        {
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("VarChar");
            mockDataRecord.Setup(x => x.GetInt16(DataRecordToColumnMapper.Columns.Size)).Returns(10);
            expected.DataType = SqlDbType.VarChar;
            expected.Size = 10;
            expected.Precision = null;

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DataRecordToColumnSizeNVarChar()
        {
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("NVarChar");
            mockDataRecord.Setup(x => x.GetInt16(DataRecordToColumnMapper.Columns.Size)).Returns(10);
            expected.DataType = SqlDbType.NVarChar;
            expected.Size = 5;
            expected.Precision = null;

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.AreEqual(expected, actual);
        }
    }
}
