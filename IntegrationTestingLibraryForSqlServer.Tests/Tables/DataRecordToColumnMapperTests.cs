using Xunit;
using System.Data;
using Moq;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class DataRecordToColumnMapperTests
    {
        private DataRecordToColumnMapper mapper = new DataRecordToColumnMapper();
        private Mock<IDataRecord> mockDataRecord;
        private ColumnDefinition expected;
        
        public DataRecordToColumnMapperTests()
        {
            mockDataRecord = new Mock<IDataRecord>();
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.Name)).Returns("r1");
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("Int");
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsNullable)).Returns(false);
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsIdentity)).Returns(false);

            expected = new IntegerColumnDefinition("r1", SqlDbType.Int)
            {
                AllowNulls = false,
            };
        }

        [Fact]
        public void DataRecordToColumnDefinition()
        {
            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DataRecordToColumnDefinitionNullable()
        {
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsNullable)).Returns(true);
            expected.AllowNulls = true;

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DataRecordToColumnIdentity()
        {
            mockDataRecord.Setup(x => x.GetBoolean(DataRecordToColumnMapper.Columns.IsIdentity)).Returns(true);
            mockDataRecord.Setup(x => x.GetDecimal(DataRecordToColumnMapper.Columns.IdentitySeed)).Returns(5);

            expected = new IntegerColumnDefinition("r1", SqlDbType.Int)
            {
                IdentitySeed = 5,
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DataRecordToColumnSizeVarChar()
        {
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("VarChar");
            mockDataRecord.Setup(x => x.GetInt16(DataRecordToColumnMapper.Columns.Size)).Returns(10);
            expected = new StringColumnDefinition("r1", SqlDbType.VarChar)
            {
                AllowNulls = false,
                Size = 10
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DataRecordToColumnSizeNVarChar()
        {
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("NVarChar");
            mockDataRecord.Setup(x => x.GetInt16(DataRecordToColumnMapper.Columns.Size)).Returns(10);
            expected = new StringColumnDefinition("r1", SqlDbType.NVarChar)
            {
                AllowNulls = false,
                Size = 5
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DataRecordToColumnMaxSizeVarChar()
        {
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("VarChar");
            mockDataRecord.Setup(x => x.GetInt16(DataRecordToColumnMapper.Columns.Size)).Returns(0);
            expected = new StringColumnDefinition("r1", SqlDbType.VarChar)
            {
                AllowNulls = false,
                Size = 0,
                IsMaximumSize = true
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DataRecordToColumnMaxAltSizeVarChar()
        {
            mockDataRecord.Setup(x => x.GetString(DataRecordToColumnMapper.Columns.DataType)).Returns("VarChar");
            mockDataRecord.Setup(x => x.GetInt16(DataRecordToColumnMapper.Columns.Size)).Returns(-1);
            expected = new StringColumnDefinition("r1", SqlDbType.VarChar)
            {
                AllowNulls = false,
                Size = -1,
                IsMaximumSize = true
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.Equal(expected, actual);
        }
        [Fact]
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
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(mockDataRecord.Object);

            Assert.Equal(expected, actual);
        }
    }
}
