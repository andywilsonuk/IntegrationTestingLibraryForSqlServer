using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class StandardProcedureParameterTests
    {
        private const string ParameterName = "@p1";
        private StandardProcedureParameter parameter = new StandardProcedureParameter(ParameterName, SqlDbType.DateTime, ParameterDirection.InputOutput);

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.DateTime, parameter.DataType.SqlType);
            Assert.AreEqual(ParameterName, parameter.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorInvalidDataTypeThrows()
        {
            new StandardProcedureParameter(ParameterName, SqlDbType.Decimal, ParameterDirection.InputOutput);
        }
    }
}
