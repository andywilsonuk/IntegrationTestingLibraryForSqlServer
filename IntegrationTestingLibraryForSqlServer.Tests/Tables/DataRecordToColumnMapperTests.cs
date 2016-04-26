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
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("Int");
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsNullable)).Returns(false);
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsIdentity)).Returns(false);

            expected = new ColumnDefinition("r1", SqlDbType.Int)
            {
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
        public void DataRecordToColumnIdentity()
        {
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsIdentity)).Returns(true);
            mockDataRecord.Setup(x => x.GetDecimal(DataRecordToColumnMapper.Columns.IdentitySeed)).Returns(5);

            expected = new IntegerColumnDefinition("r1", SqlDbType.Int)
            {
                IdentitySeed = 5,
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DataRecordToColumnSizeVarChar()
        {
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("VarChar");
            mockDataRecord.Setup(x => x.GetInt16(DataRecordToColumnMapper.Columns.Size)).Returns(10);
            expected = new ColumnDefinition("r1", SqlDbType.VarChar)
            {
                AllowNulls = false,
                Size = 10
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DataRecordToColumnSizeNVarChar()
        {
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("NVarChar");
            mockDataRecord.Setup(x => x.GetInt16(DataRecordToColumnMapper.Columns.Size)).Returns(10);
            expected = new ColumnDefinition("r1", SqlDbType.NVarChar)
            {
                AllowNulls = false,
                Size = 5
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DataRecordToColumnDecimal()
        {
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("Decimal");
            mockDataRecord.Setup(x => x.GetByte(DataRecordToColumnMapper.Columns.Precision)).Returns(10);
            mockDataRecord.Setup(x => x.GetByte(DataRecordToColumnMapper.Columns.Scale)).Returns(2);
            var expected = new DecimalColumnDefinition("r1")
            {
                Precision = 10,
                Scale = 2,
                AllowNulls = false,
                Size = 10
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.AreEqual(expected, actual);
        }
    }
}
