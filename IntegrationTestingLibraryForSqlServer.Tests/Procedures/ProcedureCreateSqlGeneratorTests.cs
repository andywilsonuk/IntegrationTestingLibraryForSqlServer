using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class ProcedureCreateSqlGeneratorTests
    {
        private ProcedureDefinition procedure = new ProcedureDefinition("testproc") { Body = "return 5" };

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
            procedure.Parameters.Add(new StandardProcedureParameter("id", SqlDbType.Int, ParameterDirection.Input));
            string expected = "create procedure [dbo].[testproc] @id Int as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithMultipleParameters()
        {
            procedure.Parameters.Add(new StandardProcedureParameter("id", SqlDbType.Int, ParameterDirection.Input));
            procedure.Parameters.Add(new SizeableProcedureParameter("name", SqlDbType.NVarChar, ParameterDirection.Input));
            string expected = "create procedure [dbo].[testproc] @id Int,@name NVarChar(1) as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithNVarCharParameter()
        {
            procedure.Parameters.Add(new SizeableProcedureParameter("name", SqlDbType.NVarChar, ParameterDirection.Input) { Size = 100 });
            string expected = "create procedure [dbo].[testproc] @name NVarChar(100) as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithNVarCharNoSizeParameter()
        {
            procedure.Parameters.Add(new SizeableProcedureParameter("name", SqlDbType.NVarChar, ParameterDirection.Input));
            string expected = "create procedure [dbo].[testproc] @name NVarChar(1) as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithMaxSizeNVarCharParameter()
        {
            procedure.Parameters.Add(new SizeableProcedureParameter("name", SqlDbType.NVarChar, ParameterDirection.Input) { IsMaximumSize = true });
            string expected = "create procedure [dbo].[testproc] @name NVarChar(max) as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithDecimalParameter()
        {
            procedure.Parameters.Add(new DecimalProcedureParameter("money", ParameterDirection.Input) { Precision = 10, Scale = 5 });
            string expected = "create procedure [dbo].[testproc] @money Decimal(10,5) as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithOutputParameter()
        {
            procedure.Parameters.Add(new StandardProcedureParameter("id", SqlDbType.Int, ParameterDirection.Output));
            string expected = "create procedure [dbo].[testproc] @id Int OUTPUT as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }
    }
}
