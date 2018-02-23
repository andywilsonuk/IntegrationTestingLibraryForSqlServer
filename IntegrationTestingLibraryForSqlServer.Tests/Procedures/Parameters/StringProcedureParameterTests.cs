using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class StringProcedureParameterTests
    {
        private const string parameterName = "@p1";
        private StringProcedureParameter parameter = new StringProcedureParameter(parameterName, SqlDbType.VarChar, ParameterDirection.Input);

        [Fact]
        public void ConstructorBasics()
        {
            Assert.Equal(SqlDbType.VarChar, parameter.DataType.SqlType);
            Assert.Equal(parameterName, parameter.Name);
            Assert.Equal(1, parameter.Size);
        }

        [Fact]
        public void ConstructorWithWrongDataTypeThrows()
        {
            Assert.Throws<ArgumentException>(() => new StringProcedureParameter(parameterName, SqlDbType.Int, ParameterDirection.Input));
        }
    }
}
