using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataCaseSensitiveStringValuePipeElementComparerTests
    {
        private TableDataCaseSensitiveStringValuePipeElementComparer comparer = new TableDataCaseSensitiveStringValuePipeElementComparer();

        [Fact]
        public void StringXStringYMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = "a" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void StringXStringYNoMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = "A" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void NullXStringYMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = (string)null, Y = "a" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [Fact]
        public void StringXNullYMatch()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = (string)null };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NotYetCompared, args.MatchStatus);
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
