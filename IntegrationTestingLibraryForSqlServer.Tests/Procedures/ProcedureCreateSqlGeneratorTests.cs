using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class ProcedureCreateSqlGeneratorTests
    {
        private const string ProcedureName = "[dbo].[testproc]";
        private const string IntegerColumnName = "@c1";
        private const string StringColumnName = "@c2";
        private const string ProcedureBody = "return 5";
        private ProcedureDefinition procedure = new ProcedureDefinition(ProcedureName) { Body = ProcedureBody };

        [Fact]
        public void CreateProcedureNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProcedureCreateSqlGenerator().Sql(null));
        }

        [Fact]
        public void CreateProcedureWithoutBodyThrowsException()
        {
            procedure.Body = null;
            Assert.Throws<ArgumentException>(() => new ProcedureCreateSqlGenerator().Sql(procedure));
        }

        [Fact]
        public void CreateProcedureWithSingleParameter()
        {
            procedure.Parameters.AddInteger(IntegerColumnName, SqlDbType.Int).Direction = ParameterDirection.Input;
           
            string expected = $"create procedure {ProcedureName} {IntegerColumnName} Int as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateProcedureWithMultipleParameters()
        {
            procedure.Parameters.AddInteger(IntegerColumnName, SqlDbType.Int).Direction = ParameterDirection.Input;
            procedure.Parameters.AddString(StringColumnName, SqlDbType.NVarChar).Direction = ParameterDirection.Input;
            string expected = $"create procedure {ProcedureName} {IntegerColumnName} Int,{StringColumnName} NVarChar(1) as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateProcedureWithNVarCharParameter()
        {
            var parameter = procedure.Parameters.AddString(StringColumnName, SqlDbType.NVarChar);
            parameter.Direction = ParameterDirection.Input;
            parameter.Size = 100;
            string expected = $"create procedure {ProcedureName} {StringColumnName} NVarChar(100) as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateProcedureWithNVarCharNoSizeParameter()
        {
            procedure.Parameters.AddString(StringColumnName, SqlDbType.NVarChar).Direction = ParameterDirection.Input;
            string expected = $"create procedure {ProcedureName} {StringColumnName} NVarChar(1) as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateProcedureWithMaxSizeNVarCharParameter()
        {
            var parameter = procedure.Parameters.AddString(StringColumnName, SqlDbType.NVarChar);
            parameter.Direction = ParameterDirection.Input;
            parameter.IsMaximumSize = true;
            string expected = $"create procedure {ProcedureName} {StringColumnName} NVarChar(max) as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateProcedureWithDecimalParameter()
        {
            var parameter = procedure.Parameters.AddDecimal("money");
            parameter.Direction = ParameterDirection.Input;
            parameter.Precision = 10;
            parameter.Scale = 5;
            string expected = $"create procedure {ProcedureName} @money Decimal(10,5) as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateProcedureWithOutputParameter()
        {
            procedure.Parameters.AddInteger(IntegerColumnName, SqlDbType.Int).Direction = ParameterDirection.Output;
            string expected = $"create procedure {ProcedureName} {IntegerColumnName} Int OUTPUT as begin {ProcedureBody} end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.Equal(expected, actual);
        }
    }
}
