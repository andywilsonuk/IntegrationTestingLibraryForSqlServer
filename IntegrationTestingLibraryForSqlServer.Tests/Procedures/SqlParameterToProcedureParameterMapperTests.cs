using Xunit;
using System.Data;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class SqlParameterToProcedureParameterMapperTests
    {
        [Fact]
        public void ProcedureParameterFromSqlParameterDecimal()
        {
            // Arrange
            var sqlParameter = new SqlParameter("p1", SqlDbType.Decimal) { Precision = 10, Scale = 5, Direction = ParameterDirection.Input };
            var expected = new DecimalProcedureParameter("p1", ParameterDirection.Input) { Precision = 10, Scale = 5, };
            var mapper = new SqlParameterToProcedureParameterMapper();

            // Act
            ProcedureParameter actual = mapper.FromSqlParameter(sqlParameter);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProcedureParameterFromSqlParameterVarChar()
        {
            // Arrange
            SqlParameter sqlParameter = new SqlParameter
            {
                ParameterName = "@p1",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Size = 10
            };
            var mapper = new SqlParameterToProcedureParameterMapper();

            // Act
            var parameter = mapper.FromSqlParameter(sqlParameter) as VariableSizeProcedureParameter;

            // Assert
            Assert.NotNull(parameter);
            Assert.Equal(sqlParameter.ParameterName, parameter.Name);
            Assert.Equal(sqlParameter.SqlDbType, parameter.DataType.SqlType);
            Assert.Equal(10, parameter.Size);
            Assert.Equal(sqlParameter.Direction, parameter.Direction);
        }

        [Fact]
        public void ProcedureParameterFromSqlParameterDateTime()
        {
            // Arrange
            SqlParameter sqlParameter = new SqlParameter
            {
                ParameterName = "@p1",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input
            };
            var mapper = new SqlParameterToProcedureParameterMapper();

            // Act
            var parameter = mapper.FromSqlParameter(sqlParameter);

            // Assert
            Assert.Equal(sqlParameter.ParameterName, parameter.Name);
            Assert.Equal(sqlParameter.SqlDbType, parameter.DataType.SqlType);
            Assert.Equal(sqlParameter.Direction, parameter.Direction);
        }
    }
}
