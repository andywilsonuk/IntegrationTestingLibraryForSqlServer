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
        public void ExceptReturnValue_ExcludesOneItem_CountOne()
        {
            var parameters = new ProcedureParameterCollection();
            parameters.Add(MockProcedureParameter.GetParameter(parameterName));
            parameters.AddReturnValue();

            var actual = parameters.ExceptReturnValue;

            Assert.AreEqual(2, parameters.Count);
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.ToList().Count);
        }

        [TestMethod]
        public void ExceptReturnValue_NoReturnParameter_CountOne()
        {
            var parameters = new ProcedureParameterCollection();
            parameters.Add(MockProcedureParameter.GetParameter(parameterName));

            var actual = parameters.ExceptReturnValue;

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.ToList().Count);
        }

        [TestMethod]
        public void ExceptReturnValue_NoParameters_CountZero()
        {
            var parameters = new ProcedureParameterCollection();

            var actual = parameters.ExceptReturnValue;

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
            string actual = ProcedureParameterCollection.GetName(MockProcedureParameter.GetParameter(parameterName));

            Assert.AreEqual(parameterName, actual);
        }

        [TestMethod]
        public void AddReturnValue_Empty_CountOne()
        {
            var parameters = new ProcedureParameterCollection();

            parameters.AddReturnValue();

            Assert.AreEqual(1, parameters.Count);
        }

        [TestMethod]
        public void AddReturnValue_AlreadyExists_CountOne()
        {
            var parameters = new ProcedureParameterCollection();
            parameters.Add(new IntegerProcedureParameter("retVal", SqlDbType.Int, ParameterDirection.ReturnValue));

            parameters.AddReturnValue();

            Assert.AreEqual(1, parameters.Count);
        }
    }
}
