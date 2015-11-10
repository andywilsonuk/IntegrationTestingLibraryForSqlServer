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
            int? expected = DataTypeDefaults.DefaultStringSize;

            int? actual = dataTypeDefaults.DefaultSize;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DefaultSizeForDecimalDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);
            int? expected = DataTypeDefaults.DefaultDecimalSize;

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
        public void DefaultDecimalPlacesForDecimalDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);
            int? expected = DataTypeDefaults.DefaultNumberOfDecimalPlaces;

            byte? actual = dataTypeDefaults.DefaultDecimalPlaces;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DefaultDecimalPlacesForIntegerDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Int);
            int? expected = null;

            byte? actual = dataTypeDefaults.DefaultDecimalPlaces;

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

            bool actual = dataTypeDefaults.IsSizeEqual(null, DataTypeDefaults.DefaultStringSize);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeEqualsForEquivalentRightDefaultedStringLikeDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsSizeEqual(DataTypeDefaults.DefaultStringSize, null);

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
        public void SizeEqualsForEqualMaximumSizeNegative1()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsSizeEqual(-1, -1);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeEqualsForEquivalentMaximumSizeLeft0()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsSizeEqual(0, -1);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeEqualsForEquivalentMaximumSizeRight0()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.NVarChar);

            bool actual = dataTypeDefaults.IsSizeEqual(-1, 0);

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

            bool actual = dataTypeDefaults.AreDecimalPlacesEqual(null, DataTypeDefaults.DefaultNumberOfDecimalPlaces);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void DecimalPlacesEqualsForRightDefaultedEquivalentDecimalDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Decimal);

            bool actual = dataTypeDefaults.AreDecimalPlacesEqual(DataTypeDefaults.DefaultNumberOfDecimalPlaces, null);

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

            Assert.IsTrue(dataTypeDefaults.IsSizeAllowed);
        }

        [TestMethod]
        public void IsSizeAllowedForIntegerDataType()
        {
            var dataTypeDefaults = new DataTypeDefaults(SqlDbType.Int);

            Assert.IsFalse(dataTypeDefaults.IsSizeAllowed);
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
    }
}
