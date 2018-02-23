using Xunit;
using System.Linq;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class ProcedureParameterFactoryTests
    {
        [Fact]
        public void FromRawDateTime()
        {
            var source = new ProcedureParameterRaw
            {
                Name = "@p1",
                DataType = "DateTime",
                Direction = ParameterDirection.InputOutput,
            };
            var factory = new ProcedureParameterFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.Single(actual);
            Assert.Equal(source.Name, actual[0].Name);
            Assert.IsType<StandardProcedureParameter>(actual[0]);
            Assert.Equal(SqlDbType.DateTime, actual[0].DataType.SqlType);
            Assert.Equal(source.Direction, actual[0].Direction);
        }

        [Fact]
        public void FromRawDecimal()
        {
            var source = new ProcedureParameterRaw
            {
                Name = "C1",
                DataType = "Decimal",
                Size = 10,
                DecimalPlaces = 2
            };
            var factory = new ProcedureParameterFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.Equal(SqlDbType.Decimal, actual[0].DataType.SqlType);
            Assert.IsType<DecimalProcedureParameter>(actual[0]);
            Assert.Equal(source.Size, ((DecimalProcedureParameter)actual[0]).Precision);
            Assert.Equal(source.DecimalPlaces, ((DecimalProcedureParameter)actual[0]).Scale);
        }
        [Fact]
        public void FromRawNumeric()
        {
            var source = new ProcedureParameterRaw
            {
                Name = "C1",
                DataType = "Numeric",
            };
            var factory = new ProcedureParameterFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.Equal(SqlDbType.Decimal, actual[0].DataType.SqlType);
        }
        [Fact]
        public void FromRawStringWithSize()
        {
            var source = new ProcedureParameterRaw
            {
                Name = "C1",
                DataType = "VarChar",
                Size = 10
            };
            var factory = new ProcedureParameterFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.Single(actual);
            Assert.Equal(SqlDbType.VarChar, actual[0].DataType.SqlType);
            Assert.IsAssignableFrom<VariableSizeProcedureParameter>(actual[0]);
            Assert.Equal(source.Size, ((VariableSizeProcedureParameter)actual[0]).Size);
        }

        [Fact]
        public void FromDataTypeBinary()
        {
            var factory = new ProcedureParameterFactory();
            DataType dataType = new DataType(SqlDbType.Binary);

            ProcedureParameter actual = factory.FromDataType(dataType, "n1", ParameterDirection.Input);

            Assert.IsType<BinaryProcedureParameter>(actual);
        }

        [Fact]
        public void FromDataTypeString()
        {
            var factory = new ProcedureParameterFactory();
            DataType dataType = new DataType(SqlDbType.VarChar);

            ProcedureParameter actual = factory.FromDataType(dataType, "n1", ParameterDirection.Input);

            Assert.IsType<StringProcedureParameter>(actual);
        }

        [Fact]
        public void FromDataTypeDecimal()
        {
            var factory = new ProcedureParameterFactory();
            DataType dataType = new DataType(SqlDbType.Decimal);

            ProcedureParameter actual = factory.FromDataType(dataType, "n1", ParameterDirection.Input);

            Assert.IsType<DecimalProcedureParameter>(actual);
        }

        [Fact]
        public void FromDataTypeInteger()
        {
            var factory = new ProcedureParameterFactory();
            DataType dataType = new DataType(SqlDbType.Int);

            ProcedureParameter actual = factory.FromDataType(dataType, "n1", ParameterDirection.Input);

            Assert.IsType<IntegerProcedureParameter>(actual);
        }

        [Fact]
        public void FromDataTypeDateTime()
        {
            var factory = new ProcedureParameterFactory();
            DataType dataType = new DataType(SqlDbType.DateTime);

            ProcedureParameter actual = factory.FromDataType(dataType, "n1", ParameterDirection.Input);

            Assert.IsType<StandardProcedureParameter>(actual);
        }
    }
}
