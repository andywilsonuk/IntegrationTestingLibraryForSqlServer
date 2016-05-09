using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DataTypeDefaultsTests
    {
        [TestMethod]
        public void DefaultSizeForStringLikeDataType()
        {
            var dataTypeDefaults = new DataType(SqlDbType.NVarChar);
            int? expected = DataType.DefaultSizeableSize;

            int? actual = dataTypeDefaults.DefaultSize;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DefaultSizeForDecimalDataType()
        {
            var dataTypeDefaults = new DataType(SqlDbType.Decimal);
            int? expected = DataType.DefaultPrecision;

            int? actual = dataTypeDefaults.DefaultSize;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DefaultSizeForIntegerDataType()
        {
            var dataTypeDefaults = new DataType(SqlDbType.Int);
            int? expected = null;

            int? actual = dataTypeDefaults.DefaultSize;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsSizeAllowedForStringLikeDataType()
        {
            var dataTypeDefaults = new DataType(SqlDbType.NVarChar);

            Assert.IsTrue(dataTypeDefaults.IsSizeable);
        }

        [TestMethod]
        public void IsSizeAllowedForIntegerDataType()
        {
            var dataTypeDefaults = new DataType(SqlDbType.Int);

            Assert.IsFalse(dataTypeDefaults.IsSizeable);
        }

        [TestMethod]
        public void IsUnicodeSizeAllowedForStringLikeDataType()
        {
            var dataTypeDefaults = new DataType(SqlDbType.NVarChar);

            Assert.IsTrue(dataTypeDefaults.IsUnicodeSizeAllowed);
        }

        [TestMethod]
        public void IsUnicodeSizeAllowedForCharDataType()
        {
            var dataTypeDefaults = new DataType(SqlDbType.Char);

            Assert.IsFalse(dataTypeDefaults.IsUnicodeSizeAllowed);
        }

        [TestMethod]
        public void IsMaximumSizeIndicatorFor1()
        {
            var dataTypeDefaults = new DataType(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsMaximumSizeIndicator(1);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsMaximumSizeIndicatorFor0()
        {
            var dataTypeDefaults = new DataType(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsMaximumSizeIndicator(0);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsMaximumSizeIndicatorForNull()
        {
            var dataTypeDefaults = new DataType(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsMaximumSizeIndicator(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsIntegerTrue()
        {
            var dataTypeDefaults = new DataType(SqlDbType.Int);

            bool actual = dataTypeDefaults.IsInteger;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsIntegerFalse()
        {
            var dataTypeDefaults = new DataType(SqlDbType.VarChar);

            bool actual = dataTypeDefaults.IsInteger;

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsDecimalTrue()
        {
            var dataTypeDefaults = new DataType(SqlDbType.Decimal);

            bool actual = dataTypeDefaults.IsDecimal;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsDecimalFalse()
        {
            var dataTypeDefaults = new DataType(SqlDbType.VarChar);

            bool actual = dataTypeDefaults.IsDecimal;

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsValidPrecisionTrue()
        {
            var dataTypeDefaults = new DataType(SqlDbType.Decimal);

            bool actual = dataTypeDefaults.IsValidPrecision(10);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsValidPrecisionFalseMinBound()
        {
            var dataTypeDefaults = new DataType(SqlDbType.VarChar);

            bool actual = dataTypeDefaults.IsValidPrecision(-1);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsValidPrecisionFalseMaxBound()
        {
            var dataTypeDefaults = new DataType(SqlDbType.VarChar);

            bool actual = dataTypeDefaults.IsValidPrecision(39);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ConstructorStringDataType()
        {
            var dataTypeDefaults = new DataType("VarChar");

            Assert.AreEqual(SqlDbType.VarChar, dataTypeDefaults.SqlType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorStringDataTypeBadType()
        {
            var dataTypeDefaults = new DataType("aaVarChar");
        }
        [TestMethod]
        public void ConstructorStringDataTypeNumericAsDecimal()
        {
            var dataTypeDefaults = new DataType("Numeric");

            Assert.AreEqual(SqlDbType.Decimal, dataTypeDefaults.SqlType);
        }
    }
}
