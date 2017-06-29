using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataBooleanValueComparerTests
    {
        private TableDataBooleanValueComparer comparer = new TableDataBooleanValueComparer();

        [TestMethod]
        public void BoolXBoolYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = true, Y = true };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void BoolXBoolYNotMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = true, Y = false };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringXBoolY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "true", Y = true };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void BoolXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = true, Y = "true" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "True", Y = "true" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void BoolXIntY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = true, Y = 5 };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [TestMethod]
        public void TableDataBoolAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }
    }
}
