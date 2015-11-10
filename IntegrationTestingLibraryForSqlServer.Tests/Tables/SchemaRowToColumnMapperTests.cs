using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using Moq;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class SchemaRowToColumnMapperTests
    {
        private SchemaRowToColumnMapper mapper = new SchemaRowToColumnMapper();
        private DataRow dataRow;
        private ColumnDefinition expected;
        
        [TestInitialize]
        public void TestInitialize()
        {
            var table = new DataTable();
            table.Columns.AddRange(new[] 
            { 
                new DataColumn(SchemaRowToColumnMapper.Columns.Name, typeof(string)),
                new DataColumn(SchemaRowToColumnMapper.Columns.DataType, typeof(SqlDbType)),
                new DataColumn(SchemaRowToColumnMapper.Columns.Size, typeof(int)),
                new DataColumn(SchemaRowToColumnMapper.Columns.Precision, typeof(int)),
                new DataColumn(SchemaRowToColumnMapper.Columns.DecimalPlaces, typeof(int)),
                new DataColumn(SchemaRowToColumnMapper.Columns.IsNullable, typeof(bool))
            });
            dataRow = table.NewRow();
            dataRow[SchemaRowToColumnMapper.Columns.Name] = "r1";
            dataRow[SchemaRowToColumnMapper.Columns.DataType] = SqlDbType.Decimal;
            dataRow[SchemaRowToColumnMapper.Columns.Size] = (int)10;
            dataRow[SchemaRowToColumnMapper.Columns.Precision] = (int)10;
            dataRow[SchemaRowToColumnMapper.Columns.DecimalPlaces] = (int)5;
            dataRow[SchemaRowToColumnMapper.Columns.IsNullable] = false;

            expected = new ColumnDefinition
            {
                Name = "r1",
                DataType = SqlDbType.Decimal,
                Size = 10,
                DecimalPlaces = 5,
                AllowNulls = false,
            };
        }

        [TestMethod]
        public void SchemaRowToColumnDefinition()
        {
            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SchemaRowToColumnDefinitionNullable()
        {
            dataRow[SchemaRowToColumnMapper.Columns.IsNullable]  = true;
            expected.AllowNulls = true;

            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SchemaRowToColumnPrecisionNonDecimal()
        {
            dataRow[SchemaRowToColumnMapper.Columns.DataType] = SqlDbType.Int;
            dataRow[SchemaRowToColumnMapper.Columns.Precision] = (byte)5;
            expected.DataType = SqlDbType.Int;
            expected.Size = null;
            expected.DecimalPlaces = null;

            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SchemaRowToColumnDecimalPlacesNonDecimal()
        {
            dataRow[SchemaRowToColumnMapper.Columns.DataType] = SqlDbType.Int;
            dataRow[SchemaRowToColumnMapper.Columns.DecimalPlaces] = (byte)5;
            expected.DataType = SqlDbType.Int;
            expected.Size = null;
            expected.DecimalPlaces = null;

            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SchemaRowToColumnSizeVarChar()
        {
            dataRow[SchemaRowToColumnMapper.Columns.DataType] = SqlDbType.VarChar;
            dataRow[SchemaRowToColumnMapper.Columns.Size] = (int)10;
            expected.DataType = SqlDbType.VarChar;
            expected.Size = 10;
            expected.DecimalPlaces = null;

            ColumnDefinition actual = mapper.ToColumnDefinition(dataRow);

            Assert.AreEqual(expected, actual);
        }
    }
}
