using System;
using Xunit;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class VariableSizeColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private MockVariableSizeColumnDefinition definition = MockVariableSizeColumnDefinition.GetColumn(ColumnName);

        [Fact]
        public void VariableSizeColumnDefinitionEquals()
        {
            definition.AllowNulls = false;
            definition.Size = 10;
            var other = MockVariableSizeColumnDefinition.GetColumn(ColumnName);
            other.AllowNulls = false;
            other.Size = 10;

            bool actual = definition.Equals(other);

            Assert.True(actual);
        }

        [Fact]
        public void VariableSizeColumnDefinitionNotEqualsSize()
        {
            definition.AllowNulls = false;
            definition.Size = 10;
            var other = MockVariableSizeColumnDefinition.GetColumn(ColumnName);
            other.AllowNulls = false;
            other.Size = 20;

            bool actual = definition.Equals(other);

            Assert.False(actual);
        }
        [Fact]
        public void VariableSizeColumnDefinitionEqualsMaxSize()
        {
            definition.AllowNulls = false;
            definition.IsMaximumSize = true;
            var other = MockVariableSizeColumnDefinition.GetColumn(ColumnName);
            other.AllowNulls = false;
            other.IsMaximumSize = true;

            bool actual = definition.Equals(other);

            Assert.True(actual);
        }

        [Fact]
        public void VariableSizeColumnDefinitionNegativeOneSizeSetToZero()
        {
            definition.Size = -1;

            Assert.Equal(0, definition.Size);
        }

        [Fact]
        public void VariableSizeColumnDefinitionNegativeSize()
        {
            Assert.Throws<ArgumentException>(() => definition.Size = -10);
        }

        [Fact]
        public void VariableSizeColumnDefinitionZeroAsMaxSize()
        {
            definition.Size = 0;

            Assert.True(definition.IsMaximumSize);
            Assert.Equal(0, definition.Size);
        }

        [Fact]
        public void VariableSizeColumnDefinitionNegativeOneAsMaxSize()
        {
            definition.Size = -1;

            Assert.True(definition.IsMaximumSize);
            Assert.Equal(0, definition.Size);
        }

        [Fact]
        public void VariableSizeColumnColumnDefinitionSetMaximumSize()
        {
            definition.IsMaximumSize = true;

            Assert.Equal(0, definition.Size);
        }

        [Fact]
        public void VariableSizeColumnColumnDefinitionToString()
        {
            definition.Size = 10;
            definition.AllowNulls = false;
            string expected = new StringBuilder()
                .Append("Name: " + ColumnName)
                .Append(", Type: VarChar")
                .Append(", Allow Nulls: False")
                .Append(", Size: 10")
                .ToString();

            string actual = definition.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
