using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class IntegerProcedureParameterTests
    {
        private const string parameterName = "@p1";
        private IntegerProcedureParameter parameter = new IntegerProcedureParameter(parameterName, SqlDbType.Int, ParameterDirection.Input);

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.Int, parameter.DataType.SqlType);
            Assert.AreEqual(parameterName, parameter.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWithWrongDataTypeThrows()
        {
            new IntegerProcedureParameter(parameterName, SqlDbType.DateTime, ParameterDirection.Input);
        }
    }
}
