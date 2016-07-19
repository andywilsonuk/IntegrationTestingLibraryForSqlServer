using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ProcedureParameterCollectionTests
    {
        private const string parameterName = "@p1";

        [TestMethod]
        public void Constructor_DefaultValues_Initialised()
        {
            var parameters = new ProcedureParameterCollection();

            Assert.IsNotNull(parameters);
        }

        [TestMethod]
        public void ExcludingReturnValue_ExcludesOneItem_CountOne()
        {
            var parameters = new ProcedureParameterCollection();
            parameters.Add(GetParameter(parameterName));
            parameters.Add(new MockProcedureParameter("retVal", SqlDbType.Int, ParameterDirection.ReturnValue));

            var actual = parameters.ExcludingReturnValue;

            Assert.AreEqual(2, parameters.Count);
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.ToList().Count);
        }

        [TestMethod]
        public void ExcludingReturnValue_NoReturnParameter_CountOne()
        {
            var parameters = new ProcedureParameterCollection();
            parameters.Add(GetParameter(parameterName));

            var actual = parameters.ExcludingReturnValue;

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.ToList().Count);
        }

        [TestMethod]
        public void ExcludingReturnValue_NoParameters_CountZero()
        {
            var parameters = new ProcedureParameterCollection();

            var actual = parameters.ExcludingReturnValue;

            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.ToList().Count);
        }

        [TestMethod]
        public void GetName_Null_ReturnsNull()
        {
            string actual = ProcedureParameterCollection.GetName(null);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetName_Parameter_ReturnsName()
        {
            string actual = ProcedureParameterCollection.GetName(GetParameter(parameterName));

            Assert.AreEqual(parameterName, actual);
        }

        private ProcedureParameter GetParameter(string name)
        {
            return new MockProcedureParameter(name, SqlDbType.NVarChar, ParameterDirection.Input);
        }
    }
}
