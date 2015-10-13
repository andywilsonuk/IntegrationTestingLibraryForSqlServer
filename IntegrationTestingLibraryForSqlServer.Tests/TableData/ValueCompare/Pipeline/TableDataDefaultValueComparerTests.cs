using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataDefaultValueComparerTests
    {
        private TableDataDefaultValueComparer comparer = new TableDataDefaultValueComparer();

        [TestMethod]
        public void StringMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = "a" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringNoMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = "A" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void TableDataDefaultValueAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }
    }
}
