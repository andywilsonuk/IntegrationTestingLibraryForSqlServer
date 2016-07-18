using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class StringProcedureParameterTests
    {
        private const string parameterName = "@p1";
        private StringProcedureParameter parameter = new StringProcedureParameter(parameterName, SqlDbType.VarChar, ParameterDirection.Input);

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.VarChar, parameter.DataType.SqlType);
            Assert.AreEqual(parameterName, parameter.Name);
            Assert.AreEqual(1, parameter.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWithWrongDataTypeThrows()
        {
            new StringProcedureParameter(parameterName, SqlDbType.Int, ParameterDirection.Input);
        }
    }
}
