using System;
using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataGuidValueComparerTests
    {
        private TableDataGuidValueComparer comparer = new TableDataGuidValueComparer();

        [Fact]
        public void GuidXGuidYMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}"), Y = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}") };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void GuidXGuidYNotMatching()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new Guid("{3604134B-8010-4CF4-ADDA-9130D10168AE}"), Y = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}") };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }

        [Fact]
        public void StringXGuidY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}", Y = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}") };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void GuidXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}"), Y = "{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void StringXStringY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = "23CB5003-ABDF-4CA3-B7F7-EA707D06DE40", Y = "{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}" };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.IsMatch, args.MatchStatus);
        }

        [Fact]
        public void GuidXIntY()
        {
            var args = new TableDataValueComparerPipeElementArguments { X = new Guid("{23CB5003-ABDF-4CA3-B7F7-EA707D06DE40}"), Y = 5 };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NotYetCompared, args.MatchStatus);
        }

        [Fact]
        public void TableDataGuidAlreadyCompared()
        {
            var args = new TableDataValueComparerPipeElementArguments { MatchStatus = MatchedValueComparer.NoMatch, X = null, Y = null };

            comparer.Process(args);

            Assert.Equal(MatchedValueComparer.NoMatch, args.MatchStatus);
        }
    }
}
