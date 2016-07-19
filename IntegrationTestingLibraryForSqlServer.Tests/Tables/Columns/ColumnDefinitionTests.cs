using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private ColumnDefinition definition = MockColumnDefinition.GetColumn(ColumnName);

        [TestMethod]
        public void ColumnDefinitionConstructor()
        {
            Assert.AreEqual(ColumnName, definition.Name);
            Assert.AreEqual(SqlDbType.Int, definition.DataType.SqlType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ColumnDefinitionConstructorNullName()
        {
            new MockColumnDefinition(null, SqlDbType.Int);
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
            var other = MockColumnDefinition.GetColumn("other");

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
            var other = MockColumnDefinition.GetColumn(ColumnName);
            other.AllowNulls = true;

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
                .Append(", Type: Int")
                .Append(", Allow Nulls: False")
                .ToString();

            string actual = definition.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
