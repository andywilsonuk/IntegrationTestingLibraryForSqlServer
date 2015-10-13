using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataValueComparerPipeElementArgumentsTests
    {
        [TestMethod]
        public void DefaultConstructor()
        {
            var actual = new TableDataValueComparerPipeElementArguments();

            Assert.AreEqual(MatchedValueComparer.NotYetCompared, actual.MatchStatus);
            Assert.IsFalse(actual.IsCompared);
        }

        [TestMethod]
        public void IsComparedSetOnMatch()
        {
            var actual = new TableDataValueComparerPipeElementArguments();
            actual.MatchStatus = MatchedValueComparer.IsMatch;

            Assert.IsTrue(actual.IsCompared);
        }

        [TestMethod]
        public void IsComparedSetOnNotMatch()
        {
            var actual = new TableDataValueComparerPipeElementArguments();
            actual.MatchStatus = MatchedValueComparer.NoMatch;

            Assert.IsTrue(actual.IsCompared);
        }
    }
}
