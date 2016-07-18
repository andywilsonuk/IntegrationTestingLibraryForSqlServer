﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class BinaryProcedureParameterTests
    {
        private const string parameterName = "@p1";
        private BinaryProcedureParameter parameter = new BinaryProcedureParameter(parameterName, SqlDbType.VarBinary, ParameterDirection.Input);

        [TestMethod]
        public void ConstructorBasics()
        {
            Assert.AreEqual(SqlDbType.VarBinary, parameter.DataType.SqlType);
            Assert.AreEqual(parameterName, parameter.Name);
            Assert.AreEqual(1, parameter.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWithWrongDataTypeThrows()
        {
            new BinaryProcedureParameter(parameterName, SqlDbType.Int, ParameterDirection.Input);
        }
    }
}
