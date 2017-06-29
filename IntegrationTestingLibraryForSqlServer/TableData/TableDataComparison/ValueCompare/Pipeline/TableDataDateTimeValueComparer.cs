using System;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataDateTimeValueComparer : TableDataValueComparerPipeElement
    {
        public void Process(TableDataValueComparerPipeElementArguments args)
        {
            if (args.MatchStatus != MatchedValueComparer.NotYetCompared) return;

            DateTime? x = ConvertToDateTime(args.X);
            DateTime? y = ConvertToDateTime(args.Y);

            if (!x.HasValue || !y.HasValue) return;

            args.MatchStatus = x.Value.Equals(y.Value) ? MatchedValueComparer.IsMatch : MatchedValueComparer.NoMatch;
        }

        private DateTime? ConvertToDateTime(object source)
        {
            if (source is DateTime) return (DateTime)source;

            DateTime date;
            if ((source is string) && DateTime.TryParse((string)source, out date)) return date;
    
            return null;
        }
    }
}
