﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                Name = "@p1",
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
            Assert.IsInstanceOfType(actual[0], typeof(VariableSizeProcedureParameter));
            Assert.AreEqual(source.Size, ((VariableSizeProcedureParameter)actual[0]).Size);
        }

        [TestMethod]
        public void FromDataTypeBinary()
        {
            var factory = new ProcedureParameterFactory();
            DataType dataType = new DataType(SqlDbType.Binary);

            ProcedureParameter actual = factory.FromDataType(dataType, "n1", ParameterDirection.Input);

            Assert.IsInstanceOfType(actual, typeof(BinaryProcedureParameter));
        }

        [TestMethod]
        public void FromDataTypeString()
        {
            var factory = new ProcedureParameterFactory();
            DataType dataType = new DataType(SqlDbType.VarChar);

            ProcedureParameter actual = factory.FromDataType(dataType, "n1", ParameterDirection.Input);

            Assert.IsInstanceOfType(actual, typeof(StringProcedureParameter));
        }

        [TestMethod]
        public void FromDataTypeDecimal()
        {
            var factory = new ProcedureParameterFactory();
            DataType dataType = new DataType(SqlDbType.Decimal);

            ProcedureParameter actual = factory.FromDataType(dataType, "n1", ParameterDirection.Input);

            Assert.IsInstanceOfType(actual, typeof(DecimalProcedureParameter));
        }

        [TestMethod]
        public void FromDataTypeInteger()
        {
            var factory = new ProcedureParameterFactory();
            DataType dataType = new DataType(SqlDbType.Int);

            ProcedureParameter actual = factory.FromDataType(dataType, "n1", ParameterDirection.Input);

            Assert.IsInstanceOfType(actual, typeof(IntegerProcedureParameter));
        }

        [TestMethod]
        public void FromDataTypeDateTime()
        {
            var factory = new ProcedureParameterFactory();
            DataType dataType = new DataType(SqlDbType.DateTime);

            ProcedureParameter actual = factory.FromDataType(dataType, "n1", ParameterDirection.Input);

            Assert.IsInstanceOfType(actual, typeof(StandardProcedureParameter));
        }
    }
}
