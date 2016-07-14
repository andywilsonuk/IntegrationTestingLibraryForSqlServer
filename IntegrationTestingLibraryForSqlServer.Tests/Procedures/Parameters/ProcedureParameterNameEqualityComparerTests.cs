using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ProcedureParameterNameEqualityComparerTests
    {
        private ProcedureParameterNameEqualityComparer comparer = new ProcedureParameterNameEqualityComparer();
        private const string parameterName = "p1";
        private ProcedureParameter parameter = GetParameter(parameterName);

        [TestMethod]
        public void Equals_NullX_False()
        {
            bool actual = comparer.Equals(null, parameter);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Equals_NullY_False()
        {
            bool actual = comparer.Equals(parameter, null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Equals_NullXY_True()
        {
            bool actual = comparer.Equals(null, null);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Equals_SameXY_True()
        {
            bool actual = comparer.Equals(parameter, parameter);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Equals_EquivalentY_True()
        {
            var parameterY = GetParameter(parameterName);

            bool actual = comparer.Equals(parameter, parameterY);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Equals_CaseAlternativeY_True()
        {
            var parameterY = GetParameter(parameterName.ToUpper());

            bool actual = comparer.Equals(parameter, parameterY);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Equals_DifferentNameY_False()
        {
            var parameterY = GetParameter(parameterName + "a");

            bool actual = comparer.Equals(parameter, parameterY);

            Assert.IsFalse(actual);
        }

        private static ProcedureParameter GetParameter(string name)
        {
            return new MockProcedureParameter(name, SqlDbType.NVarChar, ParameterDirection.Input);
        }
    }
}
