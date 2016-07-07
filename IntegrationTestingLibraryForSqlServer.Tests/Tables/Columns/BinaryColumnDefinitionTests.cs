using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class BinaryColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private BinaryColumnDefinition definition = new BinaryColumnDefinition(ColumnName, SqlDbType.VarBinary);

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.VarBinary, definition.DataType.SqlType);
            Assert.AreEqual(ColumnName, definition.Name);
            Assert.AreEqual(1, definition.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWithWrongDataTypeThrowsException()
        {
            definition = new BinaryColumnDefinition(ColumnName, SqlDbType.Int);
        }
    }
}
