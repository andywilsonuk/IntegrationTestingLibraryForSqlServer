using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ColumnDefinitionFactoryTests
    {
        [TestMethod]
        public void FromRawInteger()
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
            Assert.AreEqual(source.Name, actual[0].Name);
            Assert.AreEqual(SqlDbType.Int, actual[0].DataType);
            Assert.AreEqual(source.AllowNulls, actual[0].AllowNulls);
            Assert.AreEqual(source.IdentitySeed, actual[0].IdentitySeed);
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

            Assert.AreEqual(SqlDbType.Decimal, actual[0].DataType);
            Assert.AreEqual(source.Size, actual[0].Size);
            Assert.AreEqual(source.DecimalPlaces, actual[0].DecimalPlaces);
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

            Assert.AreEqual(SqlDbType.Decimal, actual[0].DataType);
        }
    }
}
