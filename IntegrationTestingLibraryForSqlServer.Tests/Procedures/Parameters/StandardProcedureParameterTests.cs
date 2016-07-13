using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class StandardProcedureParameterTests
    {
        private const string ColumnName = "c1";
        private StandardProcedureParameter column = new StandardProcedureParameter(ColumnName, SqlDbType.DateTime, ParameterDirection.InputOutput);

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.DateTime, column.DataType.SqlType);
            Assert.AreEqual(ColumnName, column.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorInvalidDataTypeThrows()
        {
            new StandardProcedureParameter(ColumnName, SqlDbType.Decimal, ParameterDirection.InputOutput);
        }
    }
}
