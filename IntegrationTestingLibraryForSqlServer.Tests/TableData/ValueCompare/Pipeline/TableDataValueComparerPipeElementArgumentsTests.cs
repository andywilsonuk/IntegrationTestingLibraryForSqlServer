using Xunit;
using IntegrationTestingLibraryForSqlServer.TableDataComparison;

namespace IntegrationTestingLibraryForSqlServer.Tests
{
    public class TableDataValueComparerPipeElementArgumentsTests
    {
        [Fact]
        public void DefaultConstructor()
        {
            var actual = new TableDataValueComparerPipeElementArguments();

            Assert.Equal(MatchedValueComparer.NotYetCompared, actual.MatchStatus);
            Assert.False(actual.IsCompared);
        }

        [Fact]
        public void IsComparedSetOnMatch()
        {
            var actual = new TableDataValueComparerPipeElementArguments
            {
                MatchStatus = MatchedValueComparer.IsMatch
            };

            Assert.True(actual.IsCompared);
        }

        [Fact]
        public void IsComparedSetOnNotMatch()
        {
            var actual = new TableDataValueComparerPipeElementArguments
            {
                MatchStatus = MatchedValueComparer.NoMatch
            };

            Assert.True(actual.IsCompared);
        }
    }
}
