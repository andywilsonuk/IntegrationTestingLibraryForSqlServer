using System;
using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataNullValueComparerTests
    {
        private TableDataNullValueComparer comparer = new TableDataNullValueComparer();

        [Fact]
        public void NullXNotNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = null, Y = "b" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void NullXNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = null, Y = null };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void NotNullXNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = null };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void NotNullXNotNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = "b" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [Fact]
        public void TableDataNullAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void DBNullXNotDBNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = DBNull.Value, Y = "b" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void DBNullXDBNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = DBNull.Value, Y = DBNull.Value };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void NotDBNullXDBNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "a", Y = DBNull.Value };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void NullXDBNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = null, Y = DBNull.Value };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DBNullXNullY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = DBNull.Value, Y = null };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }
    }
}
