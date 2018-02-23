using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class ProcedureParameterCollectionExtensionsTests
    {
        private const string ParameterName = "@p1";
        private ProcedureParameterCollection parameters = new ProcedureParameterCollection();

        [Fact]
        public void AddFromRaw()
        {
            var expected = new IntegerProcedureParameter(ParameterName, SqlDbType.Int, ParameterDirection.InputOutput);
            var source = new[] { new ProcedureParameterRaw { Name = ParameterName, DataType = "Int", Direction = ParameterDirection.InputOutput } };

            parameters.AddFromRaw(source);

            Assert.Single(parameters);
            Assert.Equal(expected, parameters[0]);
        }
        [Fact]
        public void AddBinary_Valid_Added()
        {
            var expected = new BinaryProcedureParameter(ParameterName, SqlDbType.Binary, ParameterDirection.InputOutput);

            var actual = parameters.AddBinary(ParameterName, SqlDbType.Binary);

            Assert.Single(parameters);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void AddDecimal_Valid_Added()
        {
            var expected = new DecimalProcedureParameter(ParameterName, ParameterDirection.InputOutput);

            var actual = parameters.AddDecimal(ParameterName);

            Assert.Single(parameters);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void AddInteger_Valid_Added()
        {
            var expected = new IntegerProcedureParameter(ParameterName, SqlDbType.Int, ParameterDirection.InputOutput);

            var actual = parameters.AddInteger(ParameterName, SqlDbType.Int);

            Assert.Single(parameters);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void AddString_Valid_Added()
        {
            var expected = new StringProcedureParameter(ParameterName, SqlDbType.VarChar, ParameterDirection.InputOutput);

            var actual = parameters.AddString(ParameterName, SqlDbType.VarChar);

            Assert.Single(parameters);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void AddStandard_Valid_Added()
        {
            var expected = new StandardProcedureParameter(ParameterName, SqlDbType.DateTime, ParameterDirection.InputOutput);

            var actual = parameters.AddStandard(ParameterName, SqlDbType.DateTime);

            Assert.Single(parameters);
            Assert.Equal(expected, actual);
        }
    }
}
