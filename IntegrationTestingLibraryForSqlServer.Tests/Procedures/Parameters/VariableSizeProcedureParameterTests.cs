using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class VariableSizeProcedureParameterTests
    {
        private const string ParameterName = "@p1";
        private MockVariableSizeProcedureParameter parameter = MockVariableSizeProcedureParameter.GetParameter(ParameterName);

        [TestInitialize]
        public void TestInitialize()
        {
            parameter.Size = 10;
        }

        [TestMethod]
        public void VariableSizeProcedureParameterEquals()
        {
            var other = MockVariableSizeProcedureParameter.GetParameter(ParameterName);
            other.Size = 10;

            Assert.AreEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterEqualsMaxSize()
        {
            parameter.IsMaximumSize = true;
            var other = MockVariableSizeProcedureParameter.GetParameter(ParameterName);
            other.IsMaximumSize = true;

            Assert.AreEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterNotEqualsSize()
        {
            var other = MockVariableSizeProcedureParameter.GetParameter(ParameterName);
            other.Size = 50;

            Assert.AreNotEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterNotEqualsName()
        {
            var other = MockVariableSizeProcedureParameter.GetParameter("other");
            other.Size = 10;

            Assert.AreNotEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterNotEqualsMaxSize()
        {
            var other = MockVariableSizeProcedureParameter.GetParameter(ParameterName);
            other.IsMaximumSize = true;

            Assert.AreNotEqual(parameter, other);
        }

        [TestMethod]
        public void VariableSizeProcedureParameterNotEqualsMaxSize2()
        {
            parameter.IsMaximumSize = true;
            var other = MockVariableSizeProcedureParameter.GetParameter(ParameterName);
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
            string expected = $"Name: {ParameterName}, Data type: VarChar, Direction: InputOutput, Size: 10";

            string actual = parameter.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
