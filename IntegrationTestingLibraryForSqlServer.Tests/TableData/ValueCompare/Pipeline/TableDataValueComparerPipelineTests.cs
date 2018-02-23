using System;
using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataValueComparerPipelineTests
    {
        private TableDataValueComparerPipeline comparer = new TableDataValueComparerPipeline();

        [Fact]
        public void GuidMatch()
        {
            bool actual = comparer.IsMatch(new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}"), "{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}");

            Assert.True(actual);
        }

        [Fact]
        public void DateTimeMatch()
        {
            bool actual = comparer.IsMatch(new DateTime(2000, 05, 18), "2000-05-18");

            Assert.True(actual);
        }

        [Fact]
        public void StringMatch()
        {
            bool actual = comparer.IsMatch("a", "a");

            Assert.True(actual);
        }

        [Fact]
        public void ToStringMatch()
        {
            bool actual = comparer.IsMatch(5, "5");

            Assert.True(actual);
        }

        [Fact]
        public void StringNoMatch()
        {
            bool actual = comparer.IsMatch("a", "b");

            Assert.False(actual);
        }
    }
}
