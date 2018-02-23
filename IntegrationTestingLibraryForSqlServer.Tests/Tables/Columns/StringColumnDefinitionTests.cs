using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class StringColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private StringColumnDefinition definition = new StringColumnDefinition(ColumnName, SqlDbType.NVarChar);

        [Fact]
        public void ConstructorBasics()
        {
            Assert.Equal(SqlDbType.NVarChar, definition.DataType.SqlType);
            Assert.Equal(ColumnName, definition.Name);
            Assert.Equal(1, definition.Size);
        }

        [Fact]
        public void ConstructorWithWrongDataTypeThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new StringColumnDefinition(ColumnName, SqlDbType.Int));
        }
    }
}
