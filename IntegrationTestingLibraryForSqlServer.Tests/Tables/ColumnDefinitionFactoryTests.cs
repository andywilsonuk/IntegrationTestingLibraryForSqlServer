using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ColumnDefinitionFactoryTests
    {
        [TestMethod]
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

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(source.Name, actual[0].Name);
            Assert.IsInstanceOfType(actual[0], typeof(StandardColumnDefinition));
            Assert.AreEqual(SqlDbType.DateTime, actual[0].DataType.SqlType);
            Assert.AreEqual(source.AllowNulls, actual[0].AllowNulls);
        }

        [TestMethod]
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

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(SqlDbType.Int, actual[0].DataType.SqlType);
            Assert.IsInstanceOfType(actual[0], typeof(IntegerColumnDefinition));
            Assert.AreEqual(source.IdentitySeed, ((IntegerColumnDefinition)actual[0]).IdentitySeed);
        }
        [TestMethod]
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

            Assert.AreEqual(SqlDbType.Decimal, actual[0].DataType.SqlType);
            Assert.IsInstanceOfType(actual[0], typeof(DecimalColumnDefinition));
            Assert.AreEqual(source.Size, ((DecimalColumnDefinition)actual[0]).Precision);
            Assert.AreEqual(source.DecimalPlaces, ((DecimalColumnDefinition)actual[0]).Scale);
        }
        [TestMethod]
        public void FromRawNumeric()
        {
            var source = new ColumnDefinitionRaw
            {
                Name = "C1",
                DataType = "Numeric",
            };
            var factory = new ColumnDefinitionFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.AreEqual(SqlDbType.Decimal, actual[0].DataType.SqlType);
        }
        [TestMethod]
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

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(SqlDbType.VarChar, actual[0].DataType.SqlType);
            Assert.IsInstanceOfType(actual[0], typeof(StringColumnDefinition));
            Assert.AreEqual(source.Size, ((StringColumnDefinition)actual[0]).Size);
        }

        [TestMethod]
        public void FromDataTypeBinary()
        {
            var factory = new ColumnDefinitionFactory();
            DataType dataType = new DataType(SqlDbType.Binary);

            ColumnDefinition actual = factory.FromDataType(dataType, "n1");

            Assert.IsInstanceOfType(actual, typeof(BinaryColumnDefinition));
        }

        [TestMethod]
        public void FromDataTypeString()
        {
            var factory = new ColumnDefinitionFactory();
            DataType dataType = new DataType(SqlDbType.VarChar);

            ColumnDefinition actual = factory.FromDataType(dataType, "n1");

            Assert.IsInstanceOfType(actual, typeof(StringColumnDefinition));
        }

        [TestMethod]
        public void FromDataTypeDecimal()
        {
            var factory = new ColumnDefinitionFactory();
            DataType dataType = new DataType(SqlDbType.Decimal);

            ColumnDefinition actual = factory.FromDataType(dataType, "n1");

            Assert.IsInstanceOfType(actual, typeof(DecimalColumnDefinition));
        }

        [TestMethod]
        public void FromDataTypeInteger()
        {
            var factory = new ColumnDefinitionFactory();
            DataType dataType = new DataType(SqlDbType.Int);

            ColumnDefinition actual = factory.FromDataType(dataType, "n1");

            Assert.IsInstanceOfType(actual, typeof(IntegerColumnDefinition));
        }

        [TestMethod]
        public void FromDataTypeDateTime()
        {
            var factory = new ColumnDefinitionFactory();
            DataType dataType = new DataType(SqlDbType.DateTime);

            ColumnDefinition actual = factory.FromDataType(dataType, "n1");

            Assert.IsInstanceOfType(actual, typeof(StandardColumnDefinition));
        }
    }
}
