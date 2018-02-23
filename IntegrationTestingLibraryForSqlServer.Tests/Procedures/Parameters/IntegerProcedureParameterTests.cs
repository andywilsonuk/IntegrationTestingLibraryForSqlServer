using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class IntegerProcedureParameterTests
    {
        private const string parameterName = "@p1";
        private IntegerProcedureParameter parameter = new IntegerProcedureParameter(parameterName, SqlDbType.Int, ParameterDirection.Input);

        [Fact]
        public void ConstructorBasics()
        {
            Assert.Equal(SqlDbType.Int, parameter.DataType.SqlType);
            Assert.Equal(parameterName, parameter.Name);
        }

        [Fact]
        public void ConstructorWithWrongDataTypeThrows()
        {
            Assert.Throws<ArgumentException>(() => new IntegerProcedureParameter(parameterName, SqlDbType.DateTime, ParameterDirection.Input));
        }
    }
}
