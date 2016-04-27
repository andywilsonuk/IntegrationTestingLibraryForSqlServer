using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class SizeableColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private SizeableColumnDefinition definition = new SizeableColumnDefinition(ColumnName, SqlDbType.NVarChar);

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.NVarChar, definition.DataType);
            Assert.AreEqual(ColumnName, definition.Name);
            Assert.AreEqual(1, definition.Size);
        }
        [TestMethod]
        public void SizeableColumnDefinitionEquals()
        {
            definition.AllowNulls = false;
            definition.Size = 10;
            var other = new SizeableColumnDefinition(ColumnName, SqlDbType.NVarChar)
            {
                AllowNulls = false,
                Size = 10,
            };

            bool actual = definition.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeableColumnDefinitionNotEqualsSize()
        {
            definition.AllowNulls = false;
            definition.Size = 10;
            var other = new SizeableColumnDefinition(ColumnName, SqlDbType.NVarChar)
            {
                AllowNulls = false,
                Size = 20,
            };

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void SizeableColumnDefinitionEqualsMaxSize()
        {
            definition.AllowNulls = false;
            definition.IsMaximumSize = true;
            var other = new SizeableColumnDefinition(ColumnName, SqlDbType.NVarChar)
            {
                AllowNulls = false,
                IsMaximumSize = true,
            };

            bool actual = definition.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SizeableDefinitionNegativeSize()
        {
            definition.Size = -10;
        }

        [TestMethod]
        public void SizeableDefinitionZeroAsMaxSize()
        {
            definition.Size = 0;

            Assert.IsTrue(definition.IsMaximumSize);
            Assert.AreEqual(0, definition.Size);
        }

        [TestMethod]
        public void SizeableColumnDefinitionSetMaximumSize()
        {
            definition.IsMaximumSize = true;

            Assert.AreEqual(0, definition.Size);
        }

        [TestMethod]
        public void SizeableColumnDefinitionToString()
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
