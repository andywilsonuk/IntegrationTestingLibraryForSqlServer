using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class SqlParameterToProcedureParameterMapperTests
    {
        [TestMethod]
        public void ProcedureParameterFromSqlParameterDecimal()
        {
            // Arrange
            var sqlParameter = new SqlParameter("p1", SqlDbType.Decimal) { Precision = 10, Scale = 5, Direction = ParameterDirection.Input };
            var expected = new DecimalProcedureParameter("p1", ParameterDirection.Input) { Precision = 10, Scale = 5, };
            var mapper = new SqlParameterToProcedureParameterMapper();

            // Act
            ProcedureParameter actual = mapper.FromSqlParameter(sqlParameter);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProcedureParameterFromSqlParameterVarChar()
        {
            // Arrange
            SqlParameter sqlParameter = new SqlParameter
            {
                ParameterName = "p1",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Size = 10
            };
            var mapper = new SqlParameterToProcedureParameterMapper();

            // Act
            var parameter = mapper.FromSqlParameter(sqlParameter) as VariableSizeProcedureParameter;

            // Assert
            Assert.IsNotNull(parameter);
            Assert.AreEqual(sqlParameter.ParameterName, parameter.Name);
            Assert.AreEqual(sqlParameter.SqlDbType, parameter.DataType.SqlType);
            Assert.AreEqual(10, parameter.Size);
            Assert.AreEqual(sqlParameter.Direction, parameter.Direction);
        }

        [TestMethod]
        public void ProcedureParameterFromSqlParameterDateTime()
        {
            // Arrange
            SqlParameter sqlParameter = new SqlParameter
            {
                ParameterName = "p1",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input
            };
            var mapper = new SqlParameterToProcedureParameterMapper();

            // Act
            var parameter = mapper.FromSqlParameter(sqlParameter);

            // Assert
            Assert.AreEqual(sqlParameter.ParameterName, parameter.Name);
            Assert.AreEqual(sqlParameter.SqlDbType, parameter.DataType.SqlType);
            Assert.AreEqual(sqlParameter.Direction, parameter.Direction);
        }
    }
}
