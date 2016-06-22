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
        private ColumnDefinition definition = new MockColumnDefinition(ColumnName, SqlDbType.DateTime);

        [TestMethod]
        public void ColumnDefinitionConstructor()
        {
            new MockColumnDefinition(ColumnName, SqlDbType.DateTime);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ColumnDefinitionConstructorNullName()
        {
            new MockColumnDefinition(null, SqlDbType.DateTime);
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
            var other = new MockColumnDefinition("other", SqlDbType.DateTime);

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionNotEqualsDataType()
        {
            var other = new MockColumnDefinition(ColumnName, SqlDbType.SmallDateTime);

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionNotEqualsAllowNulls()
        {
            definition.AllowNulls = false;
            var other = new MockColumnDefinition(ColumnName, SqlDbType.DateTime)
            {
                AllowNulls = true,
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
        public void ColumnDefinitionToString()
        {
            definition.AllowNulls = false;
            string expected = new StringBuilder()
                .Append("Name: " + ColumnName)
                .Append(", Type: DateTime")
                .Append(", Allow Nulls: False")
                .ToString();

            string actual = definition.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}