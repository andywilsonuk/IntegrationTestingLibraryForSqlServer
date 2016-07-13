using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class VariableSizeProcedureParameterTests
    {
        private const string ParameterName = "p1";
        private MockVariableSizeProcedureParameter parameter = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);

        [TestInitialize]
        public void TestInitialize()
        {
            parameter.Size = 10;
        }

        [TestMethod]
        public void VariableSizeProcedureParameterEquals()
        {
            var other = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);
            other.Size = 10;

            Assert.AreEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterEqualsMaxSize()
        {
            parameter.IsMaximumSize = true;
            var other = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);
            other.IsMaximumSize = true;

            Assert.AreEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterNotEqualsSize()
        {
            var other = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);
            other.Size = 50;

            Assert.AreNotEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterNotEqualsName()
        {
            var other = new MockVariableSizeProcedureParameter("other", SqlDbType.VarChar, ParameterDirection.Input);
            other.Size = 10;

            Assert.AreNotEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterNotEqualsMaxSize()
        {
            var other = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);
            other.IsMaximumSize = true;

            Assert.AreNotEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterNotEqualsMaxSize2()
        {
            parameter.IsMaximumSize = true;
            var other = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);
            other.Size = 10;

            Assert.AreNotEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterGetMaximumSize0()
        {
            parameter.Size = 0;

            Assert.IsTrue(parameter.IsMaximumSize);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterGetMaximumSizeNegativeOne()
        {
            parameter.Size = -1;

            Assert.IsTrue(parameter.IsMaximumSize);
            Assert.AreEqual(0, parameter.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void VariableSizeProcedureParameterSetSizeInvalidThrows()
        {
            parameter.Size = -2;
        }

        [TestMethod]
        public void VariableSizeProcedureParameterSetMaximumSize()
        {
            parameter.IsMaximumSize = true;

            Assert.AreEqual(0, parameter.Size);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterToString()
        {
            string expected = $"Name: {ParameterName}, Data type: VarChar, Direction: Input, Size: 10";

            string actual = parameter.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
