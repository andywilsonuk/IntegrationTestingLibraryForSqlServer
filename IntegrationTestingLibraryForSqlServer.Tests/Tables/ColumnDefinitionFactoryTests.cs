using Xunit;
using System.Linq;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class ColumnDefinitionFactoryTests
    {
        [Fact]
        public void FromRawDateTime()
        {
            var source = new ColumnDefinitionRaw
            {
                Name = "C1",
                DataType = "DateTime",
                AllowNulls = false,
            };
            var factory = new ColumnDefinitionFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.Single(actual);
            Assert.Equal(source.Name, actual[0].Name);
            Assert.IsType<StandardColumnDefinition>(actual[0]);
            Assert.Equal(SqlDbType.DateTime, actual[0].DataType.SqlType);
            Assert.Equal(source.AllowNulls, actual[0].AllowNulls);
        }

        [Fact]
        public void FromRawIntegerWithIdentity()
        {
            var source = new ColumnDefinitionRaw
            {
                Name = "C1",
                DataType = "Int",
                AllowNulls = false,
                IdentitySeed = 1
            };
            var factory = new ColumnDefinitionFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.Single(actual);
            Assert.Equal(SqlDbType.Int, actual[0].DataType.SqlType);
            Assert.IsType<IntegerColumnDefinition>(actual[0]);
            Assert.Equal(source.IdentitySeed, ((IntegerColumnDefinition)actual[0]).IdentitySeed);
        }
        [Fact]
        public void FromRawDecimal()
        {
            var source = new ColumnDefinitionRaw
            {
                Name = "C1",
                DataType = "Decimal",
                Size = 10,
                DecimalPlaces = 2
            };
            var factory = new ColumnDefinitionFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.Equal(SqlDbType.Decimal, actual[0].DataType.SqlType);
            Assert.IsType<DecimalColumnDefinition>(actual[0]);
            Assert.Equal(source.Size, ((DecimalColumnDefinition)actual[0]).Precision);
            Assert.Equal(source.DecimalPlaces, ((DecimalColumnDefinition)actual[0]).Scale);
        }
        [Fact]
        public void FromRawNumeric()
        {
            var source = new ColumnDefinitionRaw
            {
                Name = "C1",
                DataType = "Numeric",
            };
            var factory = new ColumnDefinitionFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.Equal(SqlDbType.Decimal, actual[0].DataType.SqlType);
        }
        [Fact]
        public void FromRawStringWithSize()
        {
            var source = new ColumnDefinitionRaw
            {
                Name = "C1",
                DataType = "VarChar",
                Size = 10
            };
            var factory = new ColumnDefinitionFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.Single(actual);
            Assert.Equal(SqlDbType.VarChar, actual[0].DataType.SqlType);
            Assert.IsType<StringColumnDefinition>(actual[0]);
            Assert.Equal(source.Size, ((StringColumnDefinition)actual[0]).Size);
        }

        [Fact]
        public void FromDataTypeBinary()
        {
            var factory = new ColumnDefinitionFactory();
            DataType dataType = new DataType(SqlDbType.Binary);

            ColumnDefinition actual = factory.FromDataType(dataType, "n1");

            Assert.IsType<BinaryColumnDefinition>(actual);
        }

        [Fact]
        public void FromDataTypeString()
        {
            var factory = new ColumnDefinitionFactory();
            DataType dataType = new DataType(SqlDbType.VarChar);

            ColumnDefinition actual = factory.FromDataType(dataType, "n1");

            Assert.IsType<StringColumnDefinition>(actual);
        }

        [Fact]
        public void FromDataTypeDecimal()
        {
            var factory = new ColumnDefinitionFactory();
            DataType dataType = new DataType(SqlDbType.Decimal);

            ColumnDefinition actual = factory.FromDataType(dataType, "n1");

            Assert.IsType<DecimalColumnDefinition>(actual);
        }

        [Fact]
        public void FromDataTypeInteger()
        {
            var factory = new ColumnDefinitionFactory();
            DataType dataType = new DataType(SqlDbType.Int);

            ColumnDefinition actual = factory.FromDataType(dataType, "n1");

            Assert.IsType<IntegerColumnDefinition>(actual);
        }

        [Fact]
        public void FromDataTypeDateTime()
        {
            var factory = new ColumnDefinitionFactory();
            DataType dataType = new DataType(SqlDbType.DateTime);

            ColumnDefinition actual = factory.FromDataType(dataType, "n1");

            Assert.IsType<StandardColumnDefinition>(actual);
        }
    }
}
