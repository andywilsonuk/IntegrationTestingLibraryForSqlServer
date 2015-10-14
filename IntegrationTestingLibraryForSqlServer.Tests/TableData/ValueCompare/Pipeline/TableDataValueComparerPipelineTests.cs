using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataValueComparerPipelineTests
    {
        private TableDataValueComparerPipeline comparer = new TableDataValueComparerPipeline();

        [TestMethod]
        public void GuidMatch()
        {
            bool actual = comparer.IsMatch(new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}"), "{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void DateTimeMatch()
        {
            bool actual = comparer.IsMatch(new DateTime(2000, 05, 18), "2000-05-18");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void StringMatch()
        {
            bool actual = comparer.IsMatch("a", "a");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ToStringMatch()
        {
            bool actual = comparer.IsMatch(5, "5");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void StringNoMatch()
        {
            bool actual = comparer.IsMatch("a", "b");

            Assert.IsFalse(actual);
        }
    }
}
