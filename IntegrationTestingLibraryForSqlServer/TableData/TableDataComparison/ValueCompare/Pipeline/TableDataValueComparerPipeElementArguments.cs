namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataValueComparerPipeElementArguments
    {
        public TableDataValueComparerPipeElementArguments()
        {
            MatchStatus = MatchedValueComparer.NotYetCompared;
        }

        public object X { get; set; }
        public object Y { get; set; }
        public MatchedValueComparer MatchStatus { get; set; }

        public bool IsCompared
        {
            get { return MatchStatus != MatchedValueComparer.NotYetCompared; }
        }
    }

    public enum MatchedValueComparer
    {
        NotYetCompared,
        NoMatch,
        IsMatch
    }
}
