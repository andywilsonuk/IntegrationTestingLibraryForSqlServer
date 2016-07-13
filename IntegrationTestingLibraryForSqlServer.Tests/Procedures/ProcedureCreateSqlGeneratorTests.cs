using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ProcedureCreateSqlGeneratorTests
    {
        private const string ProcedureName = "[dbo].[testproc]";
        private const string IntegerColumnName = "@c1";
        private const string StringColumnName = "@c2";
        private const string ProcedureBody = "return 5";
        private ProcedureDefinition procedure = new ProcedureDefinition(ProcedureName) { Body = ProcedureBody };

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateProcedureNullThrowsException()
        {
            new ProcedureCreateSqlGenerator().Sql(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateProcedureWithoutBodyThrowsException()
        {
            procedure.Body = null;
            new ProcedureCreateSqlGenerator().Sql(procedure);
        }

        [TestMethod]
        public void CreateProcedureWithSingleParameter()
        {
            procedure.Parameters.Add(new IntegerProcedureParameter(IntegerColumnName, SqlDbType.Int, ParameterDirection.Input));
            string expected = $"create procedure {ProcedureName} {IntegerColumnName} Int as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithMultipleParameters()
        {
            procedure.Parameters.Add(new IntegerProcedureParameter(IntegerColumnName, SqlDbType.Int, ParameterDirection.Input));
            procedure.Parameters.Add(new StringProcedureParameter(StringColumnName, SqlDbType.NVarChar, ParameterDirection.Input));
            string expected = $"create procedure {ProcedureName} {IntegerColumnName} Int,{StringColumnName} NVarChar(1) as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithNVarCharParameter()
        {
            procedure.Parameters.Add(new StringProcedureParameter(StringColumnName, SqlDbType.NVarChar, ParameterDirection.Input) { Size = 100 });
            string expected = $"create procedure {ProcedureName} {StringColumnName} NVarChar(100) as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithNVarCharNoSizeParameter()
        {
            procedure.Parameters.Add(new StringProcedureParameter(StringColumnName, SqlDbType.NVarChar, ParameterDirection.Input));
            string expected = $"create procedure {ProcedureName} {StringColumnName} NVarChar(1) as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithMaxSizeNVarCharParameter()
        {
            procedure.Parameters.Add(new StringProcedureParameter(StringColumnName, SqlDbType.NVarChar, ParameterDirection.Input) { IsMaximumSize = true });
            string expected = $"create procedure {ProcedureName} {StringColumnName} NVarChar(max) as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithDecimalParameter()
        {
            procedure.Parameters.Add(new DecimalProcedureParameter("money", ParameterDirection.Input) { Precision = 10, Scale = 5 });
            string expected = $"create procedure {ProcedureName} @money Decimal(10,5) as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithOutputParameter()
        {
            procedure.Parameters.Add(new IntegerProcedureParameter(IntegerColumnName, SqlDbType.Int, ParameterDirection.Output));
            string expected = $"create procedure {ProcedureName} {IntegerColumnName} Int OUTPUT as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }
    }
}
