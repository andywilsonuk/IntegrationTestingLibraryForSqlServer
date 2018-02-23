using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class StandardProcedureParameterTests
    {
        private const string ParameterName = "@p1";
        private StandardProcedureParameter parameter = new StandardProcedureParameter(ParameterName, SqlDbType.DateTime, ParameterDirection.InputOutput);

        [Fact]
        public void ConstructorBasics()
        {
            Assert.Equal(SqlDbType.DateTime, parameter.DataType.SqlType);
            Assert.Equal(ParameterName, parameter.Name);
        }
        [Fact]
        public void ConstructorInvalidDataTypeThrows()
        {
            Assert.Throws<ArgumentException>(() => new StandardProcedureParameter(ParameterName, SqlDbType.Decimal, ParameterDirection.InputOutput));
        }
    }
}
