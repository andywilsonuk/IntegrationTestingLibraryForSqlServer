using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataNullValueComparerTests
    {
        private TableDataNullValueComparer comparer = new TableDataNullValueComparer();

        [TestMethod]
        public void NullXNotNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = null, Y = "b" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void NullXNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = null, Y = null };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void NotNullXNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = null };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void NotNullXNotNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = "b" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [TestMethod]
        public void TableDataNullAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DBNullXNotDBNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = DBNull.Value, Y = "b" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DBNullXDBNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = DBNull.Value, Y = DBNull.Value };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void NotDBNullXDBNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = DBNull.Value };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void NullXDBNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = null, Y = DBNull.Value };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void DBNullXNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = DBNull.Value, Y = null };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }
    }
}
