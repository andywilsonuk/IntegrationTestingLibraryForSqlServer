using System;
using Xunit;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class DecimalProcedureParameterTests
    {
        private const string ParameterName = "@p1";
        private DecimalProcedureParameter parameter = new DecimalProcedureParameter(ParameterName, ParameterDirection.Input)
        {
            Precision = 10,
            Scale = 2
        };

        [Fact]
        public void ProcedureParameterEquals()
        {
            var other = new DecimalProcedureParameter(ParameterName, ParameterDirection.Input)
            {
                Precision = 10,
                Scale = 2,
            };

            Assert.Equal(parameter, other);
        }


        [Fact]
        public void DecimalProcedureParameterNotEqualsPrecision()
        {
            var other = new DecimalProcedureParameter(ParameterName, ParameterDirection.Input)
            {
                Precision = 8,
                Scale = 2
            };

            Assert.NotEqual(parameter, other);
        }

        [Fact]
        public void DecimalProcedureParameterNotEqualsScale()
        {
            var other = new DecimalProcedureParameter(ParameterName, ParameterDirection.Input)
            {
                Precision = 10,
                Scale = 0,
            };

            Assert.NotEqual(parameter, other);
        }

        [Fact]
        public void ProcedureParameterToString()
        {
            string expected = $"Name: {ParameterName}, Data type: Decimal, Direction: Input, Precision: 10, Scale: 2";

            string actual = parameter.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProcedureParameterInvalidPrecisionThrows()
        {
            Assert.Throws<ArgumentException>(() => parameter.Precision = 0);
        }

        [Fact]
        public void ScaleExceedsPrecisionThrows()
        {
            parameter.Precision = 5;
            Assert.Throws<ArgumentException>(() => parameter.Scale = 6);
        }

        [Fact]
        public void ProcedureParameterPrecisionReducesScale()
        {
            parameter.Precision = 10;
            parameter.Scale = 5;
            parameter.Precision = 3;

            Assert.Equal(3, parameter.Scale);
            Assert.Equal(3, parameter.Precision);
        }
    }
}
