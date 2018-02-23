using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class SchemaRowToColumnMapperTests
    {
        private SchemaRowToColumnMapper mapper = new SchemaRowToColumnMapper();
        private DataRow dataRow;
        private ColumnDefinition expected;
        
        public SchemaRowToColumnMapperTests()
        {
            var table = new DataTable();
            table.Columns.AddRange(new[] 
            { 
                new DataColumn(SchemaRowToColumnMapper.Columns.Name, typeof(string)),
                new DataColumn(SchemaRowToColumnMapper.Columns.DataType, typeof(SqlDbType)),
                new DataColumn(SchemaRowToColumnMapper.Columns.Size, typeof(int)),
                new DataColumn(SchemaRowToColumnMapper.Columns.Precision, typeof(int)),
                new DataColumn(SchemaRowToColumnMapper.Columns.Scale, typeof(int)),
                new DataColumn(SchemaRowToColumnMapper.Columns.IsNullable, typeof(bool))
            });
            dataRow = table.NewRow();
            dataRow[SchemaRowToColumnMapper.Columns.Name] = "r1";
            dataRow[SchemaRowToColumnMapper.Columns.DataType] = SqlDbType.Int;
            dataRow[SchemaRowToColumnMapper.Columns.Size] = 10;
            dataRow[SchemaRowToColumnMapper.Columns.IsNullable] = false;

            expected = new IntegerColumnDefinition("r1", SqlDbType.Int)
            {
                AllowNulls = false,
            };
        }

        [Fact]
        public void SchemaRowToColumnDefinition()
        {
            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SchemaRowToColumnDefinitionNullable()
        {
            dataRow[SchemaRowToColumnMapper.Columns.IsNullable]  = true;
            expected.AllowNulls = true;

            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SchemaRowToColumnDecimal()
        {
            dataRow[SchemaRowToColumnMapper.Columns.DataType] = SqlDbType.Decimal;
            dataRow[SchemaRowToColumnMapper.Columns.Precision] = 10;
            dataRow[SchemaRowToColumnMapper.Columns.Scale] = 5;
            var expected = new DecimalColumnDefinition("r1")
            {
                Precision = 10,
                Scale = 5,
                AllowNulls = false
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SchemaRowToColumnSizeVarChar()
        {
            dataRow[SchemaRowToColumnMapper.Columns.DataType] = SqlDbType.VarChar;
            dataRow[SchemaRowToColumnMapper.Columns.Size] = 10;
            expected = new StringColumnDefinition("r1", SqlDbType.VarChar)
            {
                AllowNulls = false,
                Size = 10,
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SchemaRowToColumnMaxSizeVarChar()
        {
            dataRow[SchemaRowToColumnMapper.Columns.DataType] = SqlDbType.VarChar;
            dataRow[SchemaRowToColumnMapper.Columns.Size] = 0;
            expected = new StringColumnDefinition("r1", SqlDbType.VarChar)
            {
                AllowNulls = false,
                IsMaximumSize = true
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SchemaRowToColumnMaxSizeAltVarChar()
        {
            dataRow[SchemaRowToColumnMapper.Columns.DataType] = SqlDbType.VarChar;
            dataRow[SchemaRowToColumnMapper.Columns.Size] = -1;
            expected = new StringColumnDefinition("r1", SqlDbType.VarChar)
            {
                AllowNulls = false,
                IsMaximumSize = true
            };

            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.Equal(expected, actual);
        }
    }
}
