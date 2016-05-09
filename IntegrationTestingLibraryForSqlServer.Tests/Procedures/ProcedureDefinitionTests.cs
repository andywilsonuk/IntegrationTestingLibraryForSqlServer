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
            this.parameter1 = new ProcedureParameter("p1", SqlDbType.DateTime, ParameterDirection.Input);
            this.definition = new ProcedureDefinition(procedureName, new[] { parameter1 });
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
            Assert.AreEqual(procedureName, this.definition.Name);
            Assert.AreEqual(1, this.definition.Parameters.Count);
        }

        [TestMethod]
        public void ProcedureDefinitionParametersWithoutReturn()
        {
            this.definition.Parameters.Add(new ProcedureParameter("retVal", SqlDbType.Int, ParameterDirection.ReturnValue));

            Assert.AreEqual(2, this.definition.Parameters.Count);
            Assert.AreEqual(1, this.definition.ParametersWithoutReturnValue.Count());
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
            this.definition.Body = "return 5";
            string expected = new StringBuilder()
                .AppendLine("Name: " + procedureName)
                .AppendLine("Name: p1, Data type: DateTime, Direction: Input")
                .AppendLine("Body: " + "return 5")
                .ToString();

            string actual = this.definition.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProcedureDefinitionEqualsNull()
        {
            bool actual = this.definition.Equals((ProcedureDefinition)null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionEquals()
        {
            bool actual = this.definition.Equals(this.definition);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionEqualsWithBody()
        {
            this.definition.Body = "return 5";
            var other = new ProcedureDefinition(procedureName, new[] { parameter1 }) { Body = "return 5" };

            bool actual = this.definition.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionNotEqualsName()
        {
            var other = new ProcedureDefinition("other");
            bool actual = this.definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionNotEqualsBody()
        {
            this.definition.Body = "return 10";
            var other = new ProcedureDefinition(procedureName) { Body = "return 5" };

            bool actual = this.definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionNotEqualsParameters()
        {
            var parameter2 = new ProcedureParameter("p2", SqlDbType.Int, ParameterDirection.Input);
            var other = new ProcedureDefinition(procedureName, new[] { parameter1, parameter2 });
            bool actual = this.definition.Equals(other);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionEqualsWithReturnValue()
        {
            var parameter2 = new ProcedureParameter("retVal", SqlDbType.Int, ParameterDirection.ReturnValue);
            var other = new ProcedureDefinition(procedureName, new[] { parameter1, parameter2 });

            bool actual = this.definition.Equals(other);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ProcedureDefinitionVerifyEquals()
        {
            this.definition.Equals(this.definition);
        }

        [TestMethod]
        [ExpectedException(typeof(EquivalenceException))]
        public void ProcedureDefinitionVerifyNotEqualsBodyThrowsException()
        {
            this.definition.Body = "return 10";
            var other = new ProcedureDefinition(procedureName) { Body = "return 5" };

            this.definition.VerifyEqual(other);
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
