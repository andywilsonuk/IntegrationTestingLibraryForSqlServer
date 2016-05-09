using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ProcedureParameterTests
    {
        private const string ParameterName = "p1";
        private ProcedureParameter parameter = new ProcedureParameter(ParameterName, SqlDbType.DateTime, ParameterDirection.Input);

        [TestMethod]
        public void ProcedureParameterConstructor()
        {
            Assert.AreEqual(ParameterName, parameter.Name);
            Assert.AreEqual(SqlDbType.DateTime, parameter.DataType.SqlType);
            Assert.AreEqual(ParameterDirection.Input, parameter.Direction);
        }

        [TestMethod]
        public void ProcedureParameterQualifiedName()
        {
            Assert.AreEqual("@" + ParameterName, parameter.QualifiedName);
        }

        [TestMethod]
        public void ProcedureParameterQualifiedNameAlreadyQualified()
        {
            Assert.AreEqual("@" + ParameterName, parameter.QualifiedName);
        }

        [TestMethod]
        public void ProcedureParameterEquals()
        {
            var other = new ProcedureParameter(ParameterName, SqlDbType.DateTime, ParameterDirection.Input);

            bool actual = parameter.Equals(parameter);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureParameterNotEqualsNull()
        {
            bool actual = parameter.Equals(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterNotEqualsName()
        {
            var other = new ProcedureParameter("other", SqlDbType.Int, ParameterDirection.Input);
            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterNotEqualsDataType()
        {
            var other = new ProcedureParameter(ParameterName, SqlDbType.NVarChar, ParameterDirection.Input);
            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterNotEqualsDirection()
        {
            var other = new ProcedureParameter(ParameterName, SqlDbType.DateTime, ParameterDirection.Output);
            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterEqualsOutputDirection()
        {
            parameter.Direction = ParameterDirection.Output;
            var other = new ProcedureParameter(ParameterName, SqlDbType.DateTime, ParameterDirection.InputOutput);
            bool actual = parameter.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureParameterEqualsInputOutputDirection()
        {
            parameter.Direction = ParameterDirection.InputOutput;
            var other = new ProcedureParameter(ParameterName, SqlDbType.DateTime, ParameterDirection.Output);
            bool actual = parameter.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureParameterGetHashCode()
        {
            int expected = parameter.Name.ToLowerInvariant().GetHashCode();

            Assert.AreEqual(expected, parameter.GetHashCode());
        }

        [TestMethod]
        public void ProcedureParameterToString()
        {
            string expected = "Name: " + ParameterName + ", Data type: DateTime, Direction: Input";

            string actual = parameter.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
