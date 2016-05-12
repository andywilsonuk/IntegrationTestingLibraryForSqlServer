using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class DecimalProcedureParametersTests
    {
        private const string ParameterName = "p1";
        private DecimalProcedureParameter parameter = new DecimalProcedureParameter(ParameterName, ParameterDirection.Input)
        {
            Precision = 10,
            Scale = 2
        };

        [TestMethod]
        public void ProcedureParameterEquals()
        {
            var other = new DecimalProcedureParameter(ParameterName, ParameterDirection.Input)
            {
                Precision = 10,
                Scale = 2,
            };

            bool actual = parameter.Equals(parameter);

            Assert.IsTrue(actual);
        }


        [TestMethod]
        public void DecimalProcedureParameterNotEqualsPrecision()
        {
            var other = new DecimalProcedureParameter(ParameterName, ParameterDirection.Input)
            {
                Precision = 8,
                Scale = 2
            };

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void DecimalProcedureParameterNotEqualsScale()
        {
            var other = new DecimalProcedureParameter(ParameterName, ParameterDirection.Input)
            {
                Precision = 10,
                Scale = 0,
            };

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void DecimalProcedureParameterNotEqualsName()
        {
            var other = new DecimalProcedureParameter("P2", ParameterDirection.Input)
            {
                Precision = 10,
                Scale = 2,
            };

            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterToString()
        {
            string expected = "Name: " + ParameterName + ", Data type: Decimal, Direction: Input, Precision: 10, Scale: 2";

            string actual = parameter.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProcedureParameterInvalidPrecisionThrowsException()
        {
            parameter.Precision = 0;
        }

        [TestMethod]
        public void ProcedureParameterPrecisionReducesScale()
        {
            parameter.Precision = 10;
            parameter.Scale = 5;
            parameter.Precision = 3;

            Assert.AreEqual(3, parameter.Scale);
            Assert.AreEqual(3, parameter.Precision);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ScaleExceedsPrecision()
        {
            parameter.Precision = 5;
            parameter.Scale = 6;
        }
    }
}
