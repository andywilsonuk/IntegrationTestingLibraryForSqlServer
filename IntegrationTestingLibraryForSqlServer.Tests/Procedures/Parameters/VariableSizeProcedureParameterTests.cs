using System;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class VariableSizeProcedureParameterTests
    {
        private const string ParameterName = "@p1";
        private MockVariableSizeProcedureParameter parameter = MockVariableSizeProcedureParameter.GetParameter(ParameterName);

        public VariableSizeProcedureParameterTests()
        {
            parameter.Size = 10;
        }

        [Fact]
        public void VariableSizeProcedureParameterEquals()
        {
            var other = MockVariableSizeProcedureParameter.GetParameter(ParameterName);
            other.Size = 10;

            Assert.Equal(parameter, other);
        }

        [Fact]
        public void VariableSizeProcedureParameterEqualsMaxSize()
        {
            parameter.IsMaximumSize = true;
            var other = MockVariableSizeProcedureParameter.GetParameter(ParameterName);
            other.IsMaximumSize = true;

            Assert.Equal(parameter, other);
        }

        [Fact]
        public void VariableSizeProcedureParameterNotEqualsSize()
        {
            var other = MockVariableSizeProcedureParameter.GetParameter(ParameterName);
            other.Size = 50;

            Assert.NotEqual(parameter, other);
        }

        [Fact]
        public void VariableSizeProcedureParameterNotEqualsName()
        {
            var other = MockVariableSizeProcedureParameter.GetParameter("other");
            other.Size = 10;

            Assert.NotEqual(parameter, other);
        }

        [Fact]
        public void VariableSizeProcedureParameterNotEqualsMaxSize()
        {
            var other = MockVariableSizeProcedureParameter.GetParameter(ParameterName);
            other.IsMaximumSize = true;

            Assert.NotEqual(parameter, other);
        }

        [Fact]
        public void VariableSizeProcedureParameterNotEqualsMaxSize2()
        {
            parameter.IsMaximumSize = true;
            var other = MockVariableSizeProcedureParameter.GetParameter(ParameterName);
            other.Size = 10;

            Assert.NotEqual(parameter, other);
        }

        [Fact]
        public void VariableSizeProcedureParameterGetMaximumSize0()
        {
            parameter.Size = 0;

            Assert.True(parameter.IsMaximumSize);
        }

        [Fact]
        public void VariableSizeProcedureParameterGetMaximumSizeNegativeOne()
        {
            parameter.Size = -1;

            Assert.True(parameter.IsMaximumSize);
            Assert.Equal(0, parameter.Size);
        }

        [Fact]
        public void VariableSizeProcedureParameterSetSizeInvalidThrows()
        {
            Assert.Throws<ArgumentException>(() => parameter.Size = -2);
        }

        [Fact]
        public void VariableSizeProcedureParameterSetMaximumSize()
        {
            parameter.IsMaximumSize = true;

            Assert.Equal(0, parameter.Size);
        }

        [Fact]
        public void VariableSizeProcedureParameterToString()
        {
            string expected = $"Name: {ParameterName}, Data type: VarChar, Direction: InputOutput, Size: 10";

            string actual = parameter.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
