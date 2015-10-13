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
            bool actual = comparer.IsMatch(new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}"), new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}"));

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
