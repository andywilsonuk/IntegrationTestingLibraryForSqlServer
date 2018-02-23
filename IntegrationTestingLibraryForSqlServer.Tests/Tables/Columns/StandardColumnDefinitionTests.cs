using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class StandardColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private StandardColumnDefinition column = new StandardColumnDefinition(ColumnName, SqlDbType.DateTime);

        [Fact]
        public void ConstructorBasics()
        {
            Assert.Equal(SqlDbType.DateTime, column.DataType.SqlType);
            Assert.Equal(ColumnName, column.Name);
        }
        [Fact]
        public void ConstructorInvalidDataType()
        {
            Assert.Throws<ArgumentException>(() => new StandardColumnDefinition(ColumnName, SqlDbType.Int));
        }
    }
}
