using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class BinaryColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private BinaryColumnDefinition definition = new BinaryColumnDefinition(ColumnName, SqlDbType.VarBinary);

        [Fact]
        public void ConstructorBasics()
        {
            Assert.Equal(SqlDbType.VarBinary, definition.DataType.SqlType);
            Assert.Equal(ColumnName, definition.Name);
            Assert.Equal(1, definition.Size);
        }

        [Fact]
        public void ConstructorWithWrongDataTypeThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new BinaryColumnDefinition(ColumnName, SqlDbType.Int));
        }
    }
}
