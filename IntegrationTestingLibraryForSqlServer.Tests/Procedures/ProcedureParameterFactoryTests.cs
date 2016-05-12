using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ProcedureParameterFactoryTests
    {
        [TestMethod]
        public void FromRawDateTime()
        {
            var source = new ProcedureParameterRaw
            {
                Name = "C1",
                DataType = "DateTime",
                Direction = ParameterDirection.InputOutput,
            };
            var factory = new ProcedureParameterFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(source.Name, actual[0].Name);
            Assert.IsInstanceOfType(actual[0], typeof(StandardProcedureParameter));
            Assert.AreEqual(SqlDbType.DateTime, actual[0].DataType.SqlType);
            Assert.AreEqual(source.Direction, actual[0].Direction);
        }

        [TestMethod]
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

            Assert.AreEqual(SqlDbType.Decimal, actual[0].DataType.SqlType);
            Assert.IsInstanceOfType(actual[0], typeof(DecimalProcedureParameter));
            Assert.AreEqual(source.Size, ((DecimalProcedureParameter)actual[0]).Precision);
            Assert.AreEqual(source.DecimalPlaces, ((DecimalProcedureParameter)actual[0]).Scale);
        }
        [TestMethod]
        public void FromRawNumeric()
        {
            var source = new ProcedureParameterRaw
            {
                Name = "C1",
                DataType = "Numeric",
            };
            var factory = new ProcedureParameterFactory();

            var actual = factory.FromRaw(new[] { source }).ToList();

            Assert.AreEqual(SqlDbType.Decimal, actual[0].DataType.SqlType);
        }
        [TestMethod]
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

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(SqlDbType.VarChar, actual[0].DataType.SqlType);
            Assert.IsInstanceOfType(actual[0], typeof(SizeableProcedureParameter));
            Assert.AreEqual(source.Size, ((SizeableProcedureParameter)actual[0]).Size);
        }
    }
}
