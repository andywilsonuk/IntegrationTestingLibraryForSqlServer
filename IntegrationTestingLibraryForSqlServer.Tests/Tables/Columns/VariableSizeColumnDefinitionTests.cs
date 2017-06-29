using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class VariableSizeColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private MockVariableSizeColumnDefinition definition = MockVariableSizeColumnDefinition.GetColumn(ColumnName);

        [TestMethod]
        public void VariableSizeColumnDefinitionEquals()
        {
            definition.AllowNulls = false;
            definition.Size = 10;
            var other = MockVariableSizeColumnDefinition.GetColumn(ColumnName);
            other.AllowNulls = false;
            other.Size = 10;

            bool actual = definition.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void VariableSizeColumnDefinitionNotEqualsSize()
        {
            definition.AllowNulls = false;
            definition.Size = 10;
            var other = MockVariableSizeColumnDefinition.GetColumn(ColumnName);
            other.AllowNulls = false;
            other.Size = 20;

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void VariableSizeColumnDefinitionEqualsMaxSize()
        {
            definition.AllowNulls = false;
            definition.IsMaximumSize = true;
            var other = MockVariableSizeColumnDefinition.GetColumn(ColumnName);
            other.AllowNulls = false;
            other.IsMaximumSize = true;

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
                .Append(", Type: VarChar")
                .Append(", Allow Nulls: False")
                .Append(", Size: 10")
                .ToString();

            string actual = definition.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
