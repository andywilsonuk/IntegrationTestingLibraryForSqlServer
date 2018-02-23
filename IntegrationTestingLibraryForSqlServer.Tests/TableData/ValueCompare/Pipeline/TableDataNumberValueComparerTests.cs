using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataNumberValueComparerTests
    {
        private TableDataNumberValueComparer comparer = new TableDataNumberValueComparer();

        [Fact]
        public void DecimalXDecimalYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.5m, Y = 5.5m };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXDecimalYMatchingPadded()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.5m, Y = 5.50m };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXDecimalYNotMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.5m, Y = 10m };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void StringXDecimalY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "5.5", Y = 5.5m };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void StringXDecimalYPadded()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "05.50", Y = 5.5m };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.5m, Y = "5.5" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void StringXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "5.5", Y = "5.5" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXBoolY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.5m, Y = false };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [Fact]
        public void TableDataDecimalAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXIntYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5 };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXUIntYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5u };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXFloatYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5f };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXDoubleYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5d };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXLongYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5L };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXULongYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = 5UL };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXSByteYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = (sbyte)5 };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXShortYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = (short)5 };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void DecimalXUShortYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = 5.0m, Y = (ushort)5 };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }
    }
}
