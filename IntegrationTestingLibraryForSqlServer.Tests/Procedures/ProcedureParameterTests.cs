using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ProcedureParameterTests
    {
        private ProcedureParameter parameter = new ProcedureParameter(ParameterName, SqlDbType.Int, ParameterDirection.Input);
        private const string ParameterName = "p1";

        [TestMethod]
        public void ProcedureParameterConstructor()
        {
            Assert.AreEqual(ParameterName, parameter.Name);
            Assert.AreEqual(SqlDbType.Int, parameter.DataType);
            Assert.AreEqual(ParameterDirection.Input, parameter.Direction);
        }

        [TestMethod]
        public void ProcedureParameterQualifiedName()
        {
            Assert.AreEqual("@" + ParameterName, parameter.QualifiedName);
        }

        [TestMethod]
        public void ProcedureParameterIsValid()
        {
            bool actual = parameter.IsValid();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureParameterIsNotValidName()
        {
            parameter.Name = null;

            bool actual = parameter.IsValid();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterIsNotValidSize()
        {
            parameter = new ProcedureParameter() { Size = -10 };

            bool actual = parameter.IsValid();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterEnsureValid()
        {
            parameter.EnsureValid();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ProcedureParameterEnsureValidThrowsException()
        {
            parameter.Name = null;
            
            parameter.EnsureValid();
        }

        [TestMethod]
        public void ProcedureParameterEquals()
        {
            parameter.Size = 10;
            parameter.DecimalPlaces = 2;
            var other = new ProcedureParameter 
            { 
                Name = ParameterName, 
                DataType = SqlDbType.Int, 
                Direction = ParameterDirection.Input,
                Size = 10,
                DecimalPlaces = 2,
            };
            bool actual = parameter.Equals(parameter);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureParameterNotEqualsNull()
        {
            bool actual = parameter.Equals((ProcedureParameter)null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterNotEqualsName()
        {
            var other = new ProcedureParameter { Name = "other", DataType = SqlDbType.Int, Direction = ParameterDirection.Input};
            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterNotEqualsDataType()
        {
            var other = new ProcedureParameter { Name = ParameterName, DataType = SqlDbType.NVarChar, Direction = ParameterDirection.Input };
            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterNotEqualsDirection()
        {
            var other = new ProcedureParameter { Name = ParameterName, DataType = SqlDbType.Int, Direction = ParameterDirection.Output };
            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterEqualsOutputDirection()
        {
            parameter.Direction = ParameterDirection.Output;
            var other = new ProcedureParameter { Name = ParameterName, DataType = SqlDbType.Int, Direction = ParameterDirection.InputOutput };
            bool actual = parameter.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureParameterEqualsInputOutputDirection()
        {
            parameter.Direction = ParameterDirection.InputOutput;
            var other = new ProcedureParameter { Name = ParameterName, DataType = SqlDbType.Int, Direction = ParameterDirection.Output };
            bool actual = parameter.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureParameterNotEqualsSize()
        {
            parameter.Size = 10;
            var other = new ProcedureParameter 
            { 
                Name = ParameterName, 
                DataType = SqlDbType.Int, 
                Direction = ParameterDirection.Input,
                Size = 50 
            };
            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureParameterNotEqualsPrecision()
        {
            parameter.DecimalPlaces = 10;
            var other = new ProcedureParameter
            {
                Name = ParameterName,
                DataType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                DecimalPlaces = 20
            };
            bool actual = parameter.Equals(other);

            Assert.IsFalse(actual);
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
            parameter.Size = 10;
            parameter.DecimalPlaces = 5;
            string expected = "Name: " + ParameterName + ", Data type: Int, Direction: Input, Size: 10, Decimal Places: 5";

            string actual = parameter.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
