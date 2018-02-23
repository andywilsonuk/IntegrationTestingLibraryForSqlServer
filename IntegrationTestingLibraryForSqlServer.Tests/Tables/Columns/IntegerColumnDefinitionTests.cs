using System;
using Xunit;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class IntegerColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private IntegerColumnDefinition column = new IntegerColumnDefinition(ColumnName, SqlDbType.Int);

        [Fact]
        public void ConstructorBasics()
        {
            Assert.Equal(SqlDbType.Int, column.DataType.SqlType);
            Assert.Equal(ColumnName, column.Name);
        }
        [Fact]
        public void ConstructorInvalidDataType()
        {
            Assert.Throws<ArgumentException>(() => new IntegerColumnDefinition(ColumnName, SqlDbType.DateTime));
        }
        [Fact]
        public void ConstructorInvalidDataType2()
        {
            Assert.Throws<ArgumentException>(() => new IntegerColumnDefinition(ColumnName, SqlDbType.Decimal));
        }
        [Fact]
        public void IdentitySeedSetsAllowNull()
        {
            column.IdentitySeed = 1;

            Assert.False(column.AllowNulls);
        }
        [Fact]
        public void ColumnDefinitionIsNotValidWhenNullIsAllowedOnIdentity()
        {
            Assert.Throws<ArgumentException>(() => new IntegerColumnDefinition(ColumnName, SqlDbType.Int)
            {
                IdentitySeed = 1,
                AllowNulls = true
            });
        }
        [Fact]
        public void ColumnDefinitionIdentityColumnsDoNotAllowNulls()
        {
            column.AllowNulls = true;
            column.IdentitySeed = 1;

            Assert.False(column.AllowNulls);
        }
        [Fact]
        public void ColumnDefinitionNotEqualsWrongType()
        {
            var other = new StringColumnDefinition(ColumnName, SqlDbType.VarChar);

            bool actual = column.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void ColumnDefinitionNotEqualsIdentity()
        {
            column.IdentitySeed = 1;

            var other = new IntegerColumnDefinition(ColumnName, SqlDbType.Int)
            {
                AllowNulls = false
            };

            bool actual = column.Equals(other);

            Assert.False(actual);
        }
        [Fact]
        public void IntegerColumnDefinitionToString()
        {
            column.AllowNulls = false;
            column.IdentitySeed = 10;
            string expected = new StringBuilder()
                .Append("Name: c1")
                .Append(", Type: Int")
                .Append(", Allow Nulls: False")
                .Append(", Identity Seed: 10")
                .ToString();

            string actual = column.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
