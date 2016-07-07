using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class VariableSizeColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private MockVariableSizeColumnDefinition definition = new MockVariableSizeColumnDefinition(ColumnName, SqlDbType.NVarChar);

        [TestMethod]
        public void VariableSizeColumnDefinitionEquals()
        {
            definition.AllowNulls = false;
            definition.Size = 10;
            var other = new MockVariableSizeColumnDefinition(ColumnName, SqlDbType.NVarChar)
            {
                AllowNulls = false,
                Size = 10,
            };

            bool actual = definition.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void VariableSizeColumnDefinitionNotEqualsSize()
        {
            definition.AllowNulls = false;
            definition.Size = 10;
            var other = new MockVariableSizeColumnDefinition(ColumnName, SqlDbType.NVarChar)
            {
                AllowNulls = false,
                Size = 20,
            };

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void VariableSizeColumnDefinitionEqualsMaxSize()
        {
            definition.AllowNulls = false;
            definition.IsMaximumSize = true;
            var other = new MockVariableSizeColumnDefinition(ColumnName, SqlDbType.NVarChar)
            {
                AllowNulls = false,
                IsMaximumSize = true,
            };

            bool actual = definition.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void VariableSizeColumnDefinitionNegativeOneSizeSetToZero()
        {
            definition.Size = -1;

            Assert.AreEqual(0, definition.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void VariableSizeColumnDefinitionNegativeSize()
        {
            definition.Size = -10;
        }

        [TestMethod]
        public void VariableSizeColumnDefinitionZeroAsMaxSize()
        {
            definition.Size = 0;

            Assert.IsTrue(definition.IsMaximumSize);
            Assert.AreEqual(0, definition.Size);
        }

        [TestMethod]
        public void VariableSizeColumnDefinitionNegativeOneAsMaxSize()
        {
            definition.Size = -1;

            Assert.IsTrue(definition.IsMaximumSize);
            Assert.AreEqual(0, definition.Size);
        }

        [TestMethod]
        public void VariableSizeColumnColumnDefinitionSetMaximumSize()
        {
            definition.IsMaximumSize = true;

            Assert.AreEqual(0, definition.Size);
        }

        [TestMethod]
        public void VariableSizeColumnColumnDefinitionToString()
        {
            definition.Size = 10;
            definition.AllowNulls = false;
            string expected = new StringBuilder()
                .Append("Name: " + ColumnName)
                .Append(", Type: NVarChar")
                .Append(", Allow Nulls: False")
                .Append(", Size: 10")
                .ToString();

            string actual = definition.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
