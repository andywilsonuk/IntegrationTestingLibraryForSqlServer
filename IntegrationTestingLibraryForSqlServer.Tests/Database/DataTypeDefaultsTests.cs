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
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);
            int? expected = DataTypeDefaults.DefaultSizeableSize;

            int? actual = dataTypeDefaults.DefaultSize;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DefaultSizeForDecimalDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);
            int? expected = DataTypeDefaults.DefaultPrecision;

            int? actual = dataTypeDefaults.DefaultSize;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DefaultSizeForIntegerDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Int);
            int? expected = null;

            int? actual = dataTypeDefaults.DefaultSize;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SizeEqualsForEquivalentStringLikeDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsSizeEqual(5, 5);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeEqualsForNonEquivalentStringLikeDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsSizeEqual(5, 3);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SizeEqualsForEquivalentLeftDefaultedStringLikeDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsSizeEqual(null, DataTypeDefaults.DefaultSizeableSize);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeEqualsForEquivalentRightDefaultedStringLikeDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsSizeEqual(DataTypeDefaults.DefaultSizeableSize, null);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeEqualsForEquivalentBothDefaultedStringLikeDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsSizeEqual(null, null);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeEqualsForEqualMaximumSize0()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsSizeEqual(0, 0);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void DecimalPlacesEqualsForEquivalentDecimalDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);

            bool actual = dataTypeDefaults.AreDecimalPlacesEqual(5, 5);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void DecimalPlacesEqualsForNonEquivalentDecimalDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);

            bool actual = dataTypeDefaults.AreDecimalPlacesEqual(5, 3);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void DecimalPlacesEqualsForLeftDefaultedEquivalentDecimalDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);

            bool actual = dataTypeDefaults.AreDecimalPlacesEqual(null, DataTypeDefaults.DefaultScale);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void DecimalPlacesEqualsForRightDefaultedEquivalentDecimalDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);

            bool actual = dataTypeDefaults.AreDecimalPlacesEqual(DataTypeDefaults.DefaultScale, null);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void DecimalPlacesEqualsForBothDefaultedEquivalentDecimalDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);

            bool actual = dataTypeDefaults.AreDecimalPlacesEqual(null, null);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsSizeAllowedForStringLikeDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            Assert.IsTrue(dataTypeDefaults.IsSizeable);
        }

        [TestMethod]
        public void IsSizeAllowedForIntegerDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Int);

            Assert.IsFalse(dataTypeDefaults.IsSizeable);
        }

        [TestMethod]
        public void IsUnicodeSizeAllowedForStringLikeDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            Assert.IsTrue(dataTypeDefaults.IsUnicodeSizeAllowed);
        }

        [TestMethod]
        public void IsUnicodeSizeAllowedForCharDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Char);

            Assert.IsFalse(dataTypeDefaults.IsUnicodeSizeAllowed);
        }

        [TestMethod]
        public void AreDecimalPlacesAllowedForStringLikeDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            Assert.IsFalse(dataTypeDefaults.AreDecimalPlacesAllowed);
        }

        [TestMethod]
        public void AreDecimalPlacesAllowedForDecimalDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);

            Assert.IsTrue(dataTypeDefaults.AreDecimalPlacesAllowed);
        }

        [TestMethod]
        public void IsMaximumSizeIndicatorFor1()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsMaximumSizeIndicator(1);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsMaximumSizeIndicatorFor0()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsMaximumSizeIndicator(0);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsMaximumSizeIndicatorForNull()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsMaximumSizeIndicator(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsIntegerTrue()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Int);

            bool actual = dataTypeDefaults.IsInteger;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsIntegerFalse()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.VarChar);

            bool actual = dataTypeDefaults.IsInteger;

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsDecimalTrue()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);

            bool actual = dataTypeDefaults.IsDecimal;

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsDecimalFalse()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.VarChar);

            bool actual = dataTypeDefaults.IsDecimal;

            Assert.IsFalse(actual);
        }
    }
}
