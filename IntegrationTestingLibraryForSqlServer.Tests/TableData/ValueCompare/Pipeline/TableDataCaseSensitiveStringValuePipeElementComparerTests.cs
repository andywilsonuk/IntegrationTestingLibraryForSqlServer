using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    [TestClass]
    public class TableDataCaseSensitiveStringValuePipeElementComparerTests
    {
        private TableDataCaseSensitiveStringValuePipeElementComparer comparer = new TableDataCaseSensitiveStringValuePipeElementComparer();

        [TestMethod]
        public void StringXStringYMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = "a" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [TestMethod]
        public void StringXStringYNoMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = "A" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [TestMethod]
        public void NullXStringYMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = (string)null, Y = "a" };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [TestMethod]
        public void StringXNullYMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = (string)null };

            comparer.Process(args);

            Assert.AreEqual(MatchedValueComparer.NotYetCompared, args.MatchStatus);
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
