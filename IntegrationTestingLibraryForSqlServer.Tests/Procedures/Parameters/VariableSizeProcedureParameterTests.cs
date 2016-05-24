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
        public void SizeableProcedureParameterEquals()
        {
            var other = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);
            other.Size = 10;
            bool actual = parameter.Equals(parameter);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeableProcedureParameterEqualsMaxSize()
        {
            parameter.IsMaximumSize = true;
            var other = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);
            other.IsMaximumSize = true;

            bool actual = parameter.Equals(parameter);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeableProcedureParameterNotEqualsSize()
        {
            var other = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);
            other.Size = 50;

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SizeableProcedureParameterNotEqualsName()
        {
            var other = new MockVariableSizeProcedureParameter("other", SqlDbType.VarChar, ParameterDirection.Input);
            other.Size = 10;

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SizeableProcedureParameterNotEqualsMaxSize()
        {
            var other = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);
            other.IsMaximumSize = true;

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SizeableProcedureParameterNotEqualsMaxSize2()
        {
            parameter.IsMaximumSize = true;
            var other = new MockVariableSizeProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input);
            other.Size = 10;

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SizeableProcedureParameterGetMaximumSize0()
        {
            parameter.Size = 0;

            Assert.IsTrue(parameter.IsMaximumSize);
        }

        [TestMethod]
        public void SizeableProcedureParameterGetMaximumSizeNegativeOne()
        {
            parameter.Size = -1;

            Assert.IsTrue(parameter.IsMaximumSize);
            Assert.AreEqual(0, parameter.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SizeableProcedureParameterSetSizeInvalidThrows()
        {
            parameter.Size = -2;
        }

        [TestMethod]
        public void SizeableProcedureParameterSetMaximumSize()
        {
            parameter.IsMaximumSize = true;

            Assert.AreEqual(0, parameter.Size);
        }

        [TestMethod]
        public void SizeableProcedureParameterToString()
        {
            string expected = "Name: " + ParameterName + ", Data type: VarChar, Direction: Input, Size: 10";

            string actual = parameter.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
