using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataDefaultValueComparerTests
    {
        private TableDataDefaultValueComparer comparer = new TableDataDefaultValueComparer();

        [Fact]
        public void StringXIntYMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "5", Y = 5 };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void StringNoMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = "A" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void TableDataDefaultValueAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }
    }
}
