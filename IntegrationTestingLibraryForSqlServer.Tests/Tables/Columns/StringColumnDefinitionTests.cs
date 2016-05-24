using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class StringColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private StringColumnDefinition definition = new StringColumnDefinition(ColumnName, SqlDbType.NVarChar);

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.NVarChar, definition.DataType.SqlType);
            Assert.AreEqual(ColumnName, definition.Name);
            Assert.AreEqual(1, definition.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWithWrongDataTypeThrowsException()
        {
            definition = new StringColumnDefinition(ColumnName, SqlDbType.Int);
        }
    }
}
