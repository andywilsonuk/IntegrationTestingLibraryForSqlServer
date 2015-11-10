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
        [ExpectedException(typeof(ValidationException))]
        public void CreateProcedureWithInvalidParametersThrowsException()
        {
            procedure.Parameters.Add(
                new ProcedureParameter("id", SqlDbType.Int, ParameterDirection.Input) { Name = null });

            new ProcedureCreateSqlGenerator().Sql(procedure);
        }

        [TestMethod]
        public void CreateProcedureWithSingleParameter()
        {
            procedure.Parameters.Add(new ProcedureParameter("id", SqlDbType.Int, ParameterDirection.Input));
            string expected = "create procedure [testproc] @id Int as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithMultipleParameters()
        {
            procedure.Parameters.Add(new ProcedureParameter("id", SqlDbType.Int, ParameterDirection.Input));
            procedure.Parameters.Add(new ProcedureParameter("name", SqlDbType.NVarChar, ParameterDirection.Input));
            string expected = "create procedure [testproc] @id Int,@name NVarChar as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithNVarCharParameter()
        {
            procedure.Parameters.Add(new ProcedureParameter("name", SqlDbType.NVarChar, ParameterDirection.Input) { Size = 100 });
            string expected = "create procedure [testproc] @name NVarChar(100) as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithNVarCharNoSizeParameter()
        {
            procedure.Parameters.Add(new ProcedureParameter("name", SqlDbType.NVarChar, ParameterDirection.Input));
            string expected = "create procedure [testproc] @name NVarChar as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithDecimalParameter()
        {
            procedure.Parameters.Add(new ProcedureParameter("money", SqlDbType.Decimal, ParameterDirection.Input) { Size = 10, DecimalPlaces = 5 });
            string expected = "create procedure [testproc] @money Decimal(10,5) as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithDecimalNoSizeParameter()
        {
            procedure.Parameters.Add(new ProcedureParameter("money", SqlDbType.Decimal, ParameterDirection.Input) { DecimalPlaces = 5 });
            string expected = "create procedure [testproc] @money Decimal(0,5) as begin return 5 end";
            // Note this would fail when run.
            // This then forces the user to provide a valid combination of Size and Decimal Places.
            // SQL requires Precision (represented by ProcedureParameter.Size) to be >= Scale (represented by ProcedureParameter.DecimalPlaces)

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateProcedureWithOutputParameter()
        {
            procedure.Parameters.Add(new ProcedureParameter("id", SqlDbType.Int, ParameterDirection.Output));
            string expected = "create procedure [testproc] @id Int OUTPUT as begin return 5 end";

            string actual = new ProcedureCreateSqlGenerator().Sql(procedure);

            Assert.AreEqual(expected, actual);
        }
    }
}
