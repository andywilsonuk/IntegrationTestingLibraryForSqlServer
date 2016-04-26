﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class IntegerColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private IntegerColumnDefinition column = new IntegerColumnDefinition(ColumnName, SqlDbType.Int);

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.Int, column.DataType);
            Assert.AreEqual(ColumnName, column.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorInvalidDataType()
        {
            new IntegerColumnDefinition(ColumnName, SqlDbType.DateTime);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorInvalidDataType2()
        {
            new IntegerColumnDefinition(ColumnName, SqlDbType.Decimal);
        }
        [TestMethod]
        public void IdentitySeedSetsAllowNull()
        {
            column.IdentitySeed = 1;

            Assert.IsFalse(column.AllowNulls);
        }
        [TestMethod]
        public void ColumnDefinitionIsNotValidWhenNullIsAllowedOnIdentity()
        {
            var definition = new IntegerColumnDefinition(ColumnName, SqlDbType.Int)
            {
                IdentitySeed = 1,
                AllowNulls = true
            };
            
            bool actual = definition.IsValid();

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void ColumnDefinitionIsValidIdentity()
        {
            column.IdentitySeed = 1;
            column.AllowNulls = false;

            bool actual = column.IsValid();

            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void ColumnDefinitionNotEqualsWrongType()
        {
            var other = new ColumnDefinition(ColumnName, SqlDbType.VarChar);

            bool actual = column.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ColumnDefinitionNotEqualsIdentity()
        {
            column.IdentitySeed = 1;

            var other = new ColumnDefinition(ColumnName, SqlDbType.Int);

            bool actual = column.Equals(other);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void IntegerColumnDefinitionToString()
        {
            column.Size = 10;
            column.AllowNulls = false;
            column.IdentitySeed = 10;
            string expected = new StringBuilder()
                .Append("Name: c1")
                .Append(", Type: Int")
                .Append(", Size: 10")
                .Append(", Allow Nulls: False")
                .Append(", Identity Seed: 10")
                .ToString();

            string actual = column.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
