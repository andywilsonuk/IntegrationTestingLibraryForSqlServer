using System;
using Xunit;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class ColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private ColumnDefinition definition = MockColumnDefinition.GetColumn(ColumnName);

        [Fact]
        public void ColumnDefinitionConstructor()
        {
            Assert.Equal(ColumnName, definition.Name);
            Assert.Equal(SqlDbType.Int, definition.DataType.SqlType);
        }

        [Fact]
        public void ColumnDefinitionConstructorNullName()
        {
            Assert.Throws<ArgumentNullException>(() => new MockColumnDefinition(null, SqlDbType.Int));
        }


        [Fact]
        public void ColumnDefinitionNotEqualsNull()
        {
            bool actual = definition.Equals(null);

            Assert.False(actual);
        }

        [Fact]
        public void ColumnDefinitionNotEqualsName()
        {
            var other = MockColumnDefinition.GetColumn("other");

            bool actual = definition.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void ColumnDefinitionNotEqualsDataType()
        {
            var other = new MockColumnDefinition(ColumnName, SqlDbType.SmallDateTime);

            bool actual = definition.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void ColumnDefinitionNotEqualsAllowNulls()
        {
            definition.AllowNulls = false;
            var other = MockColumnDefinition.GetColumn(ColumnName);
            other.AllowNulls = true;

            bool actual = definition.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void ColumnDefinitionGetHashCode()
        {
            int expected = ColumnName.ToLowerInvariant().GetHashCode();

            int actual = definition.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ColumnDefinitionToString()
        {
            definition.AllowNulls = false;
            string expected = new StringBuilder()
                .Append("Name: " + ColumnName)
                .Append(", Type: Int")
                .Append(", Allow Nulls: False")
                .ToString();

            string actual = definition.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
