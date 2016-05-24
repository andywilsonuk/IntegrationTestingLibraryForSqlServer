using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DataTypeTests
    {
        [TestMethod]
        public void IsStringForStringLikeDataType()
        {
            var dataType = new DataType(SqlDbType.NVarChar);

            Assert.IsTrue(dataType.IsString);
        }

        [TestMethod]
        public void IsStringForIntegerDataType()
        {
            var dataType = new DataType(SqlDbType.Int);

            Assert.IsFalse(dataType.IsString);
        }

        [TestMethod]
        public void IsUnicodeStringForStringLikeDataType()
        {
            var dataType = new DataType(SqlDbType.NVarChar);

            Assert.IsTrue(dataType.IsUnicodeString);
        }

        [TestMethod]
        public void IsUnicodeStringForCharDataType()
        {
            var dataType = new DataType(SqlDbType.Char);

            Assert.IsFalse(dataType.IsUnicodeString);
        }

        [TestMethod]
        public void IsMaximumSizeIndicatorFor1()
        {
            var dataType = new DataType(SqlDbType.NVarChar);

            bool actual = dataType.IsMaximumSizeIndicator(1);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsMaximumSizeIndicatorFor0()
        {
            var dataType = new DataType(SqlDbType.NVarChar);

            bool actual = dataType.IsMaximumSizeIndicator(0);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsMaximumSizeIndicatorForNull()
        {
            var dataType = new DataType(SqlDbType.NVarChar);

            bool actual = dataType.IsMaximumSizeIndicator(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsIntegerTrue()
        {
            var dataType = new DataType(SqlDbType.Int);

            bool actual = dataType.IsInteger;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsIntegerFalse()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.IsInteger;

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsDecimalTrue()
        {
            var dataType = new DataType(SqlDbType.Decimal);

            bool actual = dataType.IsDecimal;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsDecimalFalse()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.IsDecimal;

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsBinaryTrue()
        {
            var dataType = new DataType(SqlDbType.Binary);

            bool actual = dataType.IsBinary;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsBinaryFalse()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.IsBinary;

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsValidPrecisionTrue()
        {
            var dataType = new DataType(SqlDbType.Decimal);

            bool actual = dataType.IsValidPrecision(10);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsValidPrecisionFalseMinBound()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.IsValidPrecision(0);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsValidPrecisionFalseMaxBound()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.IsValidPrecision(39);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ConstructorStringDataType()
        {
            var dataType = new DataType("VarChar");

            Assert.AreEqual(SqlDbType.VarChar, dataType.SqlType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorStringDataTypeBadType()
        {
            var dataType = new DataType("aaVarChar");
        }
        [TestMethod]
        public void ConstructorStringDataTypeNumericAsDecimal()
        {
            var dataType = new DataType("Numeric");

            Assert.AreEqual(SqlDbType.Decimal, dataType.SqlType);
        }
        [TestMethod]
        public void DataTypeGetHashCode()
        {
            var dataType = new DataType(SqlDbType.VarChar);
            int expected = SqlDbType.VarChar.GetHashCode();

            int actual = dataType.GetHashCode();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DataTypeEquals()
        {
            var dataTypeA = new DataType(SqlDbType.VarChar);
            var dataTypeB = new DataType(SqlDbType.VarChar);

            bool actual = dataTypeA.Equals(dataTypeB);

            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void DataTypeEqualsObject()
        {
            var dataTypeA = new DataType(SqlDbType.VarChar);
            var dataTypeB = new DataType(SqlDbType.VarChar);

            bool actual = dataTypeA.Equals((object)dataTypeB);

            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void DataTypeNotEqualsWrongType()
        {
            var dataType = new DataType(SqlDbType.VarChar);

            bool actual = dataType.Equals(string.Empty);

            Assert.IsFalse(actual);
        }
    }
}
