using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class DataTypeTests
    {
        [Fact]
        public void IsStringForStringLikeDataType()
        {
            var dataType = new DataType(SqlDbType.NVarChar);

            Assert.True(dataType.IsString);
        }

        [Fact]
        public void IsStringForIntegerDataType()
        {
            var dataType = new DataType(SqlDbType.Int);

            Assert.False(dataType.IsString);
        }

        [Fact]
        public void IsUnicodeStringForStringLikeDataType()
        {
            var dataType = new DataType(SqlDbType.NVarChar);

            Assert.True(dataType.IsUnicodeString);
        }

        [Fact]
        public void IsUnicodeStringForCharDataType()
        {
            var dataType = new DataType(SqlDbType.Char);

            Assert.False(dataType.IsUnicodeString);
        }

        [Fact]
        public void IsMaximumSizeIndicatorFor1()
        {
            var dataType = new DataType(SqlDbType.NVarChar);

            bool actual = dataType.IsMaximumSizeIndicator(1);

            Assert.False(actual);
        }

        [Fact]
        public void IsMaximumSizeIndicatorFor0()
        {
            var dataType = new DataType(SqlDbType.NVarChar);

            bool actual = dataType.IsMaximumSizeIndicator(0);

            Assert.True(actual);
        }

        [Fact]
        public void IsMaximumSizeIndicatorForNull()
        {
            var dataType = new DataType(SqlDbType.NVarChar);

            bool actual = dataType.IsMaximumSizeIndicator(null);

            Assert.False(actual);
        }

        [Fact]
        public void IsIntegerTrue()
        {
            var dataType = new DataType(SqlDbType.Int);

            bool actual = dataType.IsInteger;

            Assert.True(actual);
        }

        [Fact]
        public void IsIntegerFalse()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.IsInteger;

            Assert.False(actual);
        }

        [Fact]
        public void IsDecimalTrue()
        {
            var dataType = new DataType(SqlDbType.Decimal);

            bool actual = dataType.IsDecimal;

            Assert.True(actual);
        }

        [Fact]
        public void IsDecimalFalse()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.IsDecimal;

            Assert.False(actual);
        }

        [Fact]
        public void IsBinaryTrue()
        {
            var dataType = new DataType(SqlDbType.Binary);

            bool actual = dataType.IsBinary;

            Assert.True(actual);
        }

        [Fact]
        public void IsBinaryFalse()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.IsBinary;

            Assert.False(actual);
        }

        [Fact]
        public void IsValidPrecisionTrue()
        {
            var dataType = new DataType(SqlDbType.Decimal);

            bool actual = dataType.IsValidPrecision(10);

            Assert.True(actual);
        }

        [Fact]
        public void IsValidPrecisionFalseMinBound()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.IsValidPrecision(0);

            Assert.False(actual);
        }

        [Fact]
        public void IsValidPrecisionFalseMaxBound()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.IsValidPrecision(39);

            Assert.False(actual);
        }

        [Fact]
        public void ConstructorStringDataType()
        {
            var dataType = new DataType("VarChar");

            Assert.Equal(SqlDbType.VarChar, dataType.SqlType);
        }

        [Fact]
        public void ConstructorStringDataTypeBadType()
        {
            Assert.Throws<ArgumentException>(() => new DataType("aaVarChar"));
        }
        [Fact]
        public void ConstructorStringDataTypeNumericAsDecimal()
        {
            var dataType = new DataType("Numeric");

            Assert.Equal(SqlDbType.Decimal, dataType.SqlType);
        }
        [Fact]
        public void DataTypeGetHashCode()
        {
            var dataType = new DataType(SqlDbType.VarChar);
            int expected = SqlDbType.VarChar.GetHashCode();

            int actual = dataType.GetHashCode();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DataTypeEquals()
        {
            var dataTypeA = new DataType(SqlDbType.VarChar);
            var dataTypeB = new DataType(SqlDbType.VarChar);

            bool actual = dataTypeA.Equals(dataTypeB);

            Assert.True(actual);
        }
        [Fact]
        public void DataTypeEqualsObject()
        {
            var dataTypeA = new DataType(SqlDbType.VarChar);
            var dataTypeB = new DataType(SqlDbType.VarChar);

            bool actual = dataTypeA.Equals((object)dataTypeB);

            Assert.True(actual);
        }
        [Fact]
        public void DataTypeNotEqualsWrongType()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.Equals(string.Empty);

            Assert.False(actual);
        }
    }
}
