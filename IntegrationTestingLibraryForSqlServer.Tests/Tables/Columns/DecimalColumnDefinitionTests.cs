using System;
using Xunit;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class DecimalColumnDefinitionTests
    {
        private DecimalColumnDefinition column = new DecimalColumnDefinition("D1");

        [Fact]
        public void ConstructorBasics()
        {
            Assert.Equal(SqlDbType.Decimal, column.DataType.SqlType);
            Assert.Equal("D1", column.Name);
            Assert.Equal(18, column.Precision);
            Assert.Equal(0, column.Scale);
        }
        [Fact]
        public void PrecisionIncrease()
        {
            column.Precision = 28;

            Assert.Equal(28, column.Precision);
        }
        [Fact]
        public void PrecisionZero()
        {
            Assert.Throws<ArgumentException>(() => column.Precision = 0);
        }
        [Fact]
        public void PrecisionExceedsMaximum()
        {
            Assert.Throws<ArgumentException>(() => column.Precision = 39);
        }
        [Fact]
        public void PrecisionMaximum()
        {
            column.Precision = 38;

            Assert.Equal(38, column.Precision);
        }
        [Fact]
        public void PrecisionMinimum()
        {
            column.Precision = 1;

            Assert.Equal(1, column.Precision);
        }
        [Fact]
        public void PrecisionDecreaseReducesScale()
        {
            column.Scale = 5;
            column.Precision = 2;

            Assert.Equal(2, column.Precision);
            Assert.Equal(2, column.Scale);
        }
        [Fact]
        public void ScaleIncrease()
        {
            column.Scale = 2;

            Assert.Equal(2, column.Scale);
        }
        [Fact]
        public void ScaleExceedsPrecision()
        {
            column.Precision = 5;
            Assert.Throws<ArgumentException>(() => column.Scale = 6);
        }
        [Fact]
        public void DecimalColumnDefinitionToString()
        {
            column.Precision = 10;
            column.Scale = 2;
            column.AllowNulls = false;
            string expected = new StringBuilder()
                .Append("Name: D1")
                .Append(", Type: Decimal")
                .Append(", Allow Nulls: False")
                .Append(", Identity Seed: ")
                .Append(", Precision: 10")
                .Append(", Scale: 2")
                .ToString();

            string actual = column.ToString();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DecimalColumnDefinitionWithIdentityToString()
        {
            column.Precision = 10;
            column.IdentitySeed = 10;
            string expected = new StringBuilder()
                .Append("Name: D1")
                .Append(", Type: Decimal")
                .Append(", Allow Nulls: False")
                .Append(", Identity Seed: 10")
                .Append(", Precision: 10")
                .Append(", Scale: 0")
                .ToString();

            string actual = column.ToString();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void ScaleCannotBeSetWithIdentity()
        {
            column.IdentitySeed = 1;
            Assert.Throws<ArgumentException>(() => column.Scale = 2);
        }
        [Fact]
        public void EqualsInvalidBase()
        {
            DecimalColumnDefinition other = new DecimalColumnDefinition("D2");

            bool actual = column.Equals(other);

            Assert.False(actual);
        }
        [Fact]
        public void EqualsWrongTypeCompare()
        {
            ColumnDefinition other = new StandardColumnDefinition("D1", SqlDbType.DateTime);

            bool actual = column.Equals(other);

            Assert.False(actual);
        }
        [Fact]
        public void EqualsInvalidPrecision()
        {
            DecimalColumnDefinition other = new DecimalColumnDefinition(column.Name)
            {
                Precision = 10
            };
            column.Precision = 8;

            bool actual = column.Equals(other);

            Assert.False(actual);
        }
        [Fact]
        public void EqualsInvalidScale()
        {
            DecimalColumnDefinition other = new DecimalColumnDefinition(column.Name)
            {
                Scale = 2
            };
            column.Scale = 0;

            bool actual = column.Equals(other);

            Assert.False(actual);
        }
        [Fact]
        public void EqualsValid()
        {
            DecimalColumnDefinition other = new DecimalColumnDefinition(column.Name);
            column.Precision = other.Precision = 10;
            column.Scale = other.Scale = 2;

            bool actual = column.Equals(other);

            Assert.True(actual);
        }
    }
}
