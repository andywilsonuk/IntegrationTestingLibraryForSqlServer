using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataBooleanValueComparerTests
    {
        private TableDataBooleanValueComparer comparer = new TableDataBooleanValueComparer();

        [Fact]
        public void BoolXBoolYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = true, Y = true };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void BoolXBoolYNotMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = true, Y = false };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void StringXBoolY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "true", Y = true };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void BoolXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = true, Y = "true" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void StringXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "True", Y = "true" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void BoolXIntY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = true, Y = 5 };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [Fact]
        public void TableDataBoolAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }
    }
}
