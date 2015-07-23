﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private ColumnDefinition definition = new ColumnDefinition(ColumnName, SqlDbType.Int);

        [TestMethod]
        public void ColumnDefinitionConstructor()
        {
            new ColumnDefinition(ColumnName, SqlDbType.Int);
        }

        [TestMethod]
        public void ColumnDefinitionEquals()
        {
            definition.AllowNulls = false;
            definition.Size = 10;
            definition.Precision = 5;
            var other = new ColumnDefinition(ColumnName, SqlDbType.Int)
            {
                AllowNulls = false,
                Size = 10,
                Precision = 5
            };

            bool actual = definition.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ColumnDefinitionNotEqualsNull()
        {
            bool actual = definition.Equals(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionNotEqualsName()
        {
            var other = new ColumnDefinition("other", SqlDbType.Int);

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionNotEqualsDataType()
        {
            var other = new ColumnDefinition(ColumnName, SqlDbType.NVarChar);

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionNotEqualsAllowNulls()
        {
            definition.AllowNulls = false;
            var other = new ColumnDefinition(ColumnName, SqlDbType.Int)
            {
                AllowNulls = true,
            };

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionNotEqualsSize()
        {
            definition.Size = 10;
            var other = new ColumnDefinition(ColumnName, SqlDbType.Int)
            {
                Size = 20,
            };

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionNotEqualsPrecision()
        {
            definition.Precision = 5;
            var other = new ColumnDefinition(ColumnName, SqlDbType.Int)
            {
                Precision = 0
            };

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionGetHashCode()
        {
            int expected = ColumnName.ToLowerInvariant().GetHashCode();

            int actual = definition.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ColumnDefinitionIsValid()
        {
            definition.Size = 10;
            definition.Precision = 5;

            bool actual = definition.IsValid();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ColumnDefinitionIsNotValidName()
        {
            definition.Name = null;

            bool actual = definition.IsValid();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionIsNotValidSize()
        {
            definition.Size = -10;

            bool actual = definition.IsValid();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionEnsureValid()
        {
            definition.Size = 10;
            definition.Precision = 5;

            definition.EnsureValid();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ColumnDefinitionEnsureValidThrowsException()
        {
            definition.Name = null;

            definition.EnsureValid();
        }

        [TestMethod]
        public void ColumnDefinitionToString()
        {
            definition.Size = 10;
            definition.Precision = 5;
            definition.AllowNulls = false;
            string expected = new StringBuilder()
                .Append("Name: " + ColumnName)
                .Append(", Type: Int")
                .Append(", Size: 10")
                .Append(", Precision: 5")
                .Append(", Allow Nulls: False")
                .AppendLine()
                .ToString();

            string actual = definition.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
