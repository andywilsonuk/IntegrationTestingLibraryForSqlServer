using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class SizeableProcedureParametersTests
    {
        private const string ParameterName = "p1";
        private SizeableProcedureParameter parameter = new SizeableProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input)
        {
            Size = 10
        };

        [TestMethod]
        public void SizeableProcedureParameterEquals()
        {
            var other = new SizeableProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input)
            {
                Size = 10,
            };
            bool actual = parameter.Equals(parameter);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void SizeableProcedureParameterEqualsMaxSize()
        {
            parameter.IsMaximumSize = true;
            var other = new SizeableProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input)
            {
                IsMaximumSize = true,
            };
            bool actual = parameter.Equals(parameter);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorWithWrongDataTypeThrowException()
        {
            parameter = new SizeableProcedureParameter(ParameterName, SqlDbType.Int, ParameterDirection.InputOutput);
        }

        [TestMethod]
        public void SizeableProcedureParameterNotEqualsSize()
        {
            var other = new SizeableProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.Input)
            { 
                Size = 50
            };

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SizeableProcedureParameterNotEqualsName()
        {
            var other = new SizeableProcedureParameter("other", SqlDbType.VarChar, ParameterDirection.Input)
            {
                Size = 10
            };

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SizeableProcedureParameterNotEqualsMaxSize()
        {
            var other = new SizeableProcedureParameter("other", SqlDbType.VarChar, ParameterDirection.Input)
            {
                IsMaximumSize = true
            };

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void SizeableProcedureParameterNotEqualsMaxSize2()
        {
            parameter.IsMaximumSize = true;
            var other = new SizeableProcedureParameter("other", SqlDbType.VarChar, ParameterDirection.Input)
            {
                Size = 10
            };

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
