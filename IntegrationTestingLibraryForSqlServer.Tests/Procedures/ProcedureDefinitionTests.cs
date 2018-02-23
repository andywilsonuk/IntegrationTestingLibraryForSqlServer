using System;
using Xunit;
using System.Data;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class ProcedureDefinitionTests
    {
        private readonly static DatabaseObjectName procedureName = DatabaseObjectName.FromName("testproc");
        ProcedureParameter parameter1;
        ProcedureDefinition definition;

        public ProcedureDefinitionTests()
        {
            parameter1 = new StandardProcedureParameter("p1", SqlDbType.DateTime, ParameterDirection.Input);
            definition = new ProcedureDefinition(procedureName, new[] { parameter1 });
        }

        [Fact]
        public void ProcedureDefinitionConstructorNullName()
        {
            Assert.Throws<ArgumentNullException>(() => new ProcedureDefinition((DatabaseObjectName)null));
        }

        [Fact]
        public void ProcedureDefinitionConstructorNullParameters()
        {
            var definition = new ProcedureDefinition(procedureName, null);

            Assert.NotNull(definition.Parameters);
        }

        [Fact]
        public void ProcedureDefinitionConstructor()
        {
            Assert.Equal(procedureName, definition.Name);
            Assert.Single(definition.Parameters);
        }

        [Fact]
        public void ProcedureDefinitionGetHashcode()
        {
            int expected = procedureName.GetHashCode();

            Assert.Equal(expected, definition.GetHashCode());
        }

        [Fact]
        public void ProcedureDefinitionToString()
        {
            definition.Body = "return 5";
            string expected = new StringBuilder()
                .AppendLine("Name: " + procedureName)
                .AppendLine("Name: @p1, Data type: DateTime, Direction: Input")
                .AppendLine("Body: " + "return 5")
                .ToString();

            string actual = definition.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProcedureDefinitionEqualsNull()
        {
            bool actual = definition.Equals((ProcedureDefinition)null);

            Assert.False(actual);
        }

        [Fact]
        public void ProcedureDefinitionEquals()
        {
            bool actual = definition.Equals(definition);

            Assert.True(actual);
        }

        [Fact]
        public void ProcedureDefinitionEqualsAsObject()
        {
            bool actual = definition.Equals((object)definition);

            Assert.True(actual);
        }

        [Fact]
        public void ProcedureDefinitionEqualsWithBody()
        {
            definition.Body = "return 5";
            var other = new ProcedureDefinition(procedureName, new[] { parameter1 }) { Body = "return 5" };

            bool actual = definition.Equals(other);

            Assert.True(actual);
        }

        [Fact]
        public void ProcedureDefinitionNotEqualsName()
        {
            var other = new ProcedureDefinition("other");
            bool actual = definition.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void ProcedureDefinitionNotEqualsBody()
        {
            definition.Body = "return 10";
            var other = new ProcedureDefinition(procedureName) { Body = "return 5" };

            bool actual = definition.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void ProcedureDefinitionNotEqualsParameters()
        {
            var other = new ProcedureDefinition(procedureName);
            other.Parameters.Add(parameter1);
            other.Parameters.Add(MockProcedureParameter.GetParameter("p2"));
            bool actual = definition.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void Equals_YParametersContainReturnValue_True()
        {
            var other = new ProcedureDefinition(procedureName, new[] { parameter1 });
            other.Parameters.AddReturnValue();

            bool actual = definition.Equals(other);

            Assert.True(actual);
        }

        [Fact]
        public void ProcedureDefinitionVerifyEqual()
        {

            definition.VerifyEqual(definition);
        }

        [Fact]
        public void ProcedureDefinitionVerifyNotEqualsBodyThrowsException()
        {
            definition.Body = "return 10";
            var other = new ProcedureDefinition(procedureName) { Body = "return 5" };

            Assert.Throws<EquivalenceException>(() => definition.VerifyEqual(other));
        }


        [Fact]
        public void ProcedureDefinitionHasBodyFalse()
        {
            definition.Body = null;

            Assert.False(definition.HasBody);
        }

        [Fact]
        public void ProcedureDefinitionHasBodyTrue()
        {
            definition.Body = "return 10";

            Assert.True(definition.HasBody);
        }
    }
}
