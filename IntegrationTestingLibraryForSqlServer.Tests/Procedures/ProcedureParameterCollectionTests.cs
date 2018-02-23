using System.Data;
using System.Linq;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class ProcedureParameterCollectionTests
    {
        private const string parameterName = "@p1";

        [Fact]
        public void ExceptReturnValue_ExcludesOneItem_CountOne()
        {
            var parameters = new ProcedureParameterCollection
            {
                MockProcedureParameter.GetParameter(parameterName)
            };
            parameters.AddReturnValue();

            var actual = parameters.ExceptReturnValue;

            Assert.Equal(2, parameters.Count);
            Assert.NotNull(actual);
            Assert.Single(actual);
        }

        [Fact]
        public void ExceptReturnValue_NoReturnParameter_CountOne()
        {
            var parameters = new ProcedureParameterCollection
            {
                MockProcedureParameter.GetParameter(parameterName)
            };

            var actual = parameters.ExceptReturnValue;

            Assert.NotNull(actual);
            Assert.Single(actual);
        }

        [Fact]
        public void ExceptReturnValue_NoParameters_CountZero()
        {
            var parameters = new ProcedureParameterCollection();

            var actual = parameters.ExceptReturnValue;

            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void GetName_Null_ReturnsNull()
        {
            string actual = ProcedureParameterCollection.GetName(null);

            Assert.Null(actual);
        }

        [Fact]
        public void GetName_Parameter_ReturnsName()
        {
            string actual = ProcedureParameterCollection.GetName(MockProcedureParameter.GetParameter(parameterName));

            Assert.Equal(parameterName, actual);
        }

        [Fact]
        public void AddReturnValue_Empty_CountOne()
        {
            var parameters = new ProcedureParameterCollection();

            parameters.AddReturnValue();

            Assert.Single(parameters);
        }

        [Fact]
        public void AddReturnValue_AlreadyExists_CountOne()
        {
            var parameters = new ProcedureParameterCollection
            {
                new IntegerProcedureParameter("retVal", SqlDbType.Int, ParameterDirection.ReturnValue)
            };

            parameters.AddReturnValue();

            Assert.Single(parameters);
        }
    }
}
