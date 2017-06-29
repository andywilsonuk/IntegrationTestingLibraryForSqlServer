namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataBooleanValueComparer : TableDataValueComparerPipeElement
    {
        public void Process(TableDataValueComparerPipeElementArguments args)
        {
            if (args.MatchStatus != MatchedValueComparer.NotYetCompared) return;

            bool? x = ConvertToBoolean(args.X);
            bool? y = ConvertToBoolean(args.Y);

            if (!x.HasValue || !y.HasValue) return;

            args.MatchStatus = x.Value.Equals(y.Value) ? MatchedValueComparer.IsMatch : MatchedValueComparer.NoMatch;
        }

        private bool? ConvertToBoolean(object source)
        {
            if (source is bool) return (bool)source;

            bool boolean;
            if ((source is string) && bool.TryParse((string)source, out boolean)) return boolean;
    
            return null;
        }
    }
}
