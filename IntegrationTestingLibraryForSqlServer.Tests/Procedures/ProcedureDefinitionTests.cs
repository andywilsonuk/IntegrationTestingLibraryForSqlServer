using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests.Procedures
{
    [TestClass]
    public class ProcedureDefinitionTests
    {
        private readonly static DatabaseObjectName procedureName = DatabaseObjectName.FromName("testproc");
        ProcedureParameter parameter1;
        ProcedureDefinition definition;

        [TestInitialize]
        public void TestInitialize()
        {
            parameter1 = new StandardProcedureParameter("p1", SqlDbType.DateTime, ParameterDirection.Input);
            definition = new ProcedureDefinition(procedureName, new[] { parameter1 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProcedureDefinitionConstructorNullName()
        {
            var definition = new ProcedureDefinition((DatabaseObjectName)null);
        }

        [TestMethod]
        public void ProcedureDefinitionConstructorNullParameters()
        {
            var definition = new ProcedureDefinition(procedureName, null);

            Assert.IsNotNull(definition.Parameters);
            Assert.IsNotNull(definition.ParametersWithoutReturnValue);
        }

        [TestMethod]
        public void ProcedureDefinitionConstructor()
        {
            Assert.AreEqual(procedureName, definition.Name);
            Assert.AreEqual(1, definition.Parameters.Count);
        }

        [TestMethod]
        public void ProcedureDefinitionParametersWithoutReturn()
        {
            definition.Parameters.Add(new IntegerProcedureParameter("retVal", SqlDbType.Int, ParameterDirection.ReturnValue));

            Assert.AreEqual(2, definition.Parameters.Count);
            Assert.AreEqual(1, definition.ParametersWithoutReturnValue.Count());
        }

        [TestMethod]
        public void ProcedureDefinitionGetHashcode()
        {
            int expected = procedureName.GetHashCode();

            Assert.AreEqual(expected, definition.GetHashCode());
        }

        [TestMethod]
        public void ProcedureDefinitionToString()
        {
            definition.Body = "return 5";
            string expected = new StringBuilder()
                .AppendLine("Name: " + procedureName)
                .AppendLine("Name: p1, Data type: DateTime, Direction: Input")
                .AppendLine("Body: " + "return 5")
                .ToString();

            string actual = definition.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProcedureDefinitionEqualsNull()
        {
            bool actual = definition.Equals((ProcedureDefinition)null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionEquals()
        {
            bool actual = definition.Equals(definition);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionEqualsAsObject()
        {
            bool actual = definition.Equals((object)definition);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionEqualsWithBody()
        {
            definition.Body = "return 5";
            var other = new ProcedureDefinition(procedureName, new[] { parameter1 }) { Body = "return 5" };

            bool actual = definition.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionNotEqualsName()
        {
            var other = new ProcedureDefinition("other");
            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionNotEqualsBody()
        {
            definition.Body = "return 10";
            var other = new ProcedureDefinition(procedureName) { Body = "return 5" };

            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionNotEqualsParameters()
        {
            var parameter2 = new MockProcedureParameter("p2", SqlDbType.Int, ParameterDirection.Input);
            var other = new ProcedureDefinition(procedureName, new[] { parameter1, parameter2 });
            bool actual = definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionEqualsWithReturnValue()
        {
            var parameter2 = new MockProcedureParameter("retVal", SqlDbType.Int, ParameterDirection.ReturnValue);
            var other = new ProcedureDefinition(procedureName, new[] { parameter1, parameter2 });

            bool actual = definition.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionVerifyEqual()
        {

            definition.VerifyEqual(definition);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void ProcedureDefinitionVerifyNotEqualsBodyThrowsException()
        {
            definition.Body = "return 10";
            var other = new ProcedureDefinition(procedureName) { Body = "return 5" };

            definition.VerifyEqual(other);
        }


        [TestMethod]
        public void ProcedureDefinitionHasBodyFalse()
        {
            definition.Body = null;

            Assert.IsFalse(definition.HasBody);
        }

        [TestMethod]
        public void ProcedureDefinitionHasBodyTrue()
        {
            definition.Body = "return 10";

            Assert.IsTrue(definition.HasBody);
        }
    }
}
