using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class ColumnDefinitionCollectionExtensionsTests
    {
        private const string ColumnName = "C1";
        ColumnDefinitionCollection columns = new ColumnDefinitionCollection();

        [Fact]
        public void AddFromRaw()
        {
            var expected = new IntegerColumnDefinition(ColumnName, SqlDbType.Int) { AllowNulls = true };
            var source = new[] { new ColumnDefinitionRaw { Name = ColumnName, DataType = "Int", AllowNulls = true } };
            
            columns.AddFromRaw(source);

            Assert.Single(columns);
            Assert.Equal(expected, columns[0]);
        }
        [Fact]
        public void AddBinary_Valid_Added()
        {
            var expected = new BinaryColumnDefinition(ColumnName, SqlDbType.Binary);

            var actual = columns.AddBinary(ColumnName, SqlDbType.Binary);

            Assert.Single(columns);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void AddDecimal_Valid_Added()
        {
            var expected = new DecimalColumnDefinition(ColumnName);

            var actual = columns.AddDecimal(ColumnName);

            Assert.Single(columns);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void AddInteger_Valid_Added()
        {
            var expected = new IntegerColumnDefinition(ColumnName, SqlDbType.Int);

            var actual = columns.AddInteger(ColumnName, SqlDbType.Int);

            Assert.Single(columns);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void AddString_Valid_Added()
        {
            var expected = new StringColumnDefinition(ColumnName, SqlDbType.VarChar);

            var actual = columns.AddString(ColumnName, SqlDbType.VarChar);

            Assert.Single(columns);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void AddStandard_Valid_Added()
        {
            var expected = new StandardColumnDefinition(ColumnName, SqlDbType.DateTime);

            var actual = columns.AddStandard(ColumnName, SqlDbType.DateTime);

            Assert.Single(columns);
            Assert.Equal(expected, actual);
        }
    }
}
