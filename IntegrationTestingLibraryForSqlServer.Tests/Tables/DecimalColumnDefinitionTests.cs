using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DecimalColumnDefinitionTests
    {
        private DecimalColumnDefinition column = new DecimalColumnDefinition("D1");

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.Decimal, column.DataType.SqlType);
            Assert.AreEqual("D1", column.Name);
            Assert.AreEqual(18, column.Precision);
            Assert.AreEqual(0, column.Scale);
        }
        [TestMethod]
        public void PrecisionIncrease()
        {
            column.Precision = 28;

            Assert.AreEqual(28, column.Precision);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PrecisionZero()
        {
            column.Precision = 0;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PrecisionExceedsMaximum()
        {
            column.Precision = 39;
        }
        [TestMethod]
        public void PrecisionMaximum()
        {
            column.Precision = 38;

            Assert.AreEqual(38, column.Precision);
        }
        [TestMethod]
        public void PrecisionMinimum()
        {
            column.Precision = 1;

            Assert.AreEqual(1, column.Precision);
        }
        [TestMethod]
        public void PrecisionDecreaseReducesScale()
        {
            column.Scale = 5;
            column.Precision = 2;

            Assert.AreEqual(2, column.Precision);
            Assert.AreEqual(2, column.Scale);
        }
        [TestMethod]
        public void ScaleIncrease()
        {
            column.Scale = 2;

            Assert.AreEqual(2, column.Scale);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ScaleExceedsPrecision()
        {
            column.Precision = 5;
            column.Scale = 6;
        }
        [TestMethod]
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

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
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

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ScaleCannotBeSetWithIdentity()
        {
            column.IdentitySeed = 1;
            column.Scale = 2;
        }
        [TestMethod]
        public void EqualsInvalidBase()
        {
            DecimalColumnDefinition other = new DecimalColumnDefinition("D2");

            bool actual = column.Equals(other);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void EqualsWrongTypeCompare()
        {
            ColumnDefinition other = new ColumnDefinition("D1", SqlDbType.DateTime);

            bool actual = column.Equals(other);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void EqualsInvalidPrecision()
        {
            DecimalColumnDefinition other = new DecimalColumnDefinition(column.Name);
            other.Precision = 10;
            column.Precision = 8;

            bool actual = column.Equals(other);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void EqualsInvalidScale()
        {
            DecimalColumnDefinition other = new DecimalColumnDefinition(column.Name);
            other.Scale = 2;
            column.Scale = 0;

            bool actual = column.Equals(other);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void EqualsValid()
        {
            DecimalColumnDefinition other = new DecimalColumnDefinition(column.Name);
            column.Precision = other.Precision = 10;
            column.Scale = other.Scale = 2;

            bool actual = column.Equals(other);

            Assert.IsTrue(actual);
        }
    }
}
