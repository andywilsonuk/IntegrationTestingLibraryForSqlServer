using System;
using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataDateTimeValueComparerTests
    {
        private TableDataDateTimeValueComparer comparer = new TableDataDateTimeValueComparer();

        [Fact]
        public void DateTimeXDateTimeYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new DateTime(2000, 05, 18), Y = new DateTime(2000, 05, 18) };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DateTimeXDateTimeYNotMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new DateTime(2000, 05, 18), Y = new DateTime(2010, 05, 18) };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void StringXDateTimeY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "2000-05-18", Y = new DateTime(2000, 05, 18) };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DateTimeXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new DateTime(2000, 05, 18), Y = "18 May 2000" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void StringXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "18/05/2000", Y = "18 May 2000 00:00:00" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DateTimeXIntY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new DateTime(2000, 05, 18), Y = 5 };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [Fact]
        public void TableDataDateTimeAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }
    }
}
