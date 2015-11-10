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
            var expected = new ProcedureParameter("p1", SqlDbType.Decimal, ParameterDirection.Input) { Size = 10, DecimalPlaces = 5, };
            var mapper = new SqlParameterToProcedureParameterMapper();

            // Act
            ProcedureParameter actual = mapper.FromSqlParameter(sqlParameter);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProcedureParameterFromSqlParameterNonDecimalSize()
        {
            // Arrange
            SqlParameter sqlParameter = new SqlParameter
            {
                ParameterName = "p1",
                SqlDbType = SqlDbType.Int,
                Size = 10,
                Precision = 5,
                Direction = ParameterDirection.Input
            };
            var mapper = new SqlParameterToProcedureParameterMapper();

            // Act
            var parameter = mapper.FromSqlParameter(sqlParameter);

            // Assert
            Assert.AreEqual(sqlParameter.ParameterName, parameter.Name);
            Assert.AreEqual(sqlParameter.SqlDbType, parameter.DataType);
            Assert.AreEqual(null, parameter.Size);
            Assert.AreEqual(null, parameter.DecimalPlaces);
            Assert.AreEqual(sqlParameter.Direction, parameter.Direction);
        }

        [TestMethod]
        public void ProcedureParameterFromSqlParameterNonDecimalNoSize()
        {
            // Arrange
            SqlParameter sqlParameter = new SqlParameter
            {
                ParameterName = "p1",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input
            };
            var mapper = new SqlParameterToProcedureParameterMapper();

            // Act
            var parameter = mapper.FromSqlParameter(sqlParameter);

            // Assert
            Assert.AreEqual(sqlParameter.ParameterName, parameter.Name);
            Assert.AreEqual(sqlParameter.SqlDbType, parameter.DataType);
            Assert.AreEqual(null, parameter.Size);
            Assert.AreEqual(null, parameter.DecimalPlaces);
            Assert.AreEqual(sqlParameter.Direction, parameter.Direction);
        }
    }
}
