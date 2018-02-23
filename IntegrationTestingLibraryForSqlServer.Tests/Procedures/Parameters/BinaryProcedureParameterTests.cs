using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class BinaryProcedureParameterTests
    {
        private const string parameterName = "@p1";
        private BinaryProcedureParameter parameter = new BinaryProcedureParameter(parameterName, SqlDbType.VarBinary, ParameterDirection.Input);

        [Fact]
        public void ConstructorBasics()
        {
            Assert.Equal(SqlDbType.VarBinary, parameter.DataType.SqlType);
            Assert.Equal(parameterName, parameter.Name);
            Assert.Equal(1, parameter.Size);
        }

        [Fact]
        public void ConstructorWithWrongDataTypeThrows()
        {
            Assert.Throws<ArgumentException>(() => new BinaryProcedureParameter(parameterName, SqlDbType.Int, ParameterDirection.Input));
        }
    }
}
