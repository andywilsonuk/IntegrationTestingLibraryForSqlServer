using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataDateTimeValueComparerTests
    {
        private TableDataDateTimeValueComparer comparer = new TableDataDateTimeValueComparer();

        [TestMethod]
        public void DateTimeXDateTimeYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new DateTime(2000, 05, 18), Y = new DateTime(2000, 05, 18) };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DateTimeXDateTimeYNotMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new DateTime(2000, 05, 18), Y = new DateTime(2010, 05, 18) };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringXDateTimeY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "2000-05-18", Y = new DateTime(2000, 05, 18) };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DateTimeXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new DateTime(2000, 05, 18), Y = "18 May 2000" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "18/05/2000", Y = "18 May 2000 00:00:00" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DateTimeXIntY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new DateTime(2000, 05, 18), Y = 5 };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [TestMethod]
        public void TableDataDateTimeAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }
    }
}
