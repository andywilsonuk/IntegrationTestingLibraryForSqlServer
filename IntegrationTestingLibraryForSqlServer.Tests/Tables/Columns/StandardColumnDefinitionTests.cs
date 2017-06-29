using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class StandardColumnDefinitionTests
    {
        private const string ColumnName = "c1";
        private StandardColumnDefinition column = new StandardColumnDefinition(ColumnName, SqlDbType.DateTime);

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.DateTime, column.DataType.SqlType);
            Assert.AreEqual(ColumnName, column.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorInvalidDataType()
        {
            new StandardColumnDefinition(ColumnName, SqlDbType.Int);
        }
    }
}
