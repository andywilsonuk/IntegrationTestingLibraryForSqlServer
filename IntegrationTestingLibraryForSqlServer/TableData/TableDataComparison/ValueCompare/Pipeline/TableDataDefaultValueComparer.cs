namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataDefaultValueComparer : TableDataValueComparerPipeElement
    {
        public void Process(TableDataValueComparerPipeElementArguments args)
        {
            if (args.MatchStatus != MatchedValueComparer.NotYetCompared) return;

            args.MatchStatus = args.X.ToString().Equals(args.Y.ToString()) ? MatchedValueComparer.IsMatch : MatchedValueComparer.NoMatch;
        }
    }
}
