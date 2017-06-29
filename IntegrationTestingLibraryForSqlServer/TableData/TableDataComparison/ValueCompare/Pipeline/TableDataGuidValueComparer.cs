using System;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataGuidValueComparer : TableDataValueComparerPipeElement
    {
        public void Process(TableDataValueComparerPipeElementArguments args)
        {
            if (args.MatchStatus != MatchedValueComparer.NotYetCompared) return;

            Guid? x = ConvertToGuid(args.X);
            Guid? y = ConvertToGuid(args.Y);

            if (!x.HasValue || !y.HasValue) return;

            args.MatchStatus = x.Value.Equals(y.Value) ? MatchedValueComparer.IsMatch : MatchedValueComparer.NoMatch;
        }

        private Guid? ConvertToGuid(object source)
        {
            if (source is Guid) return (Guid)source;

            Guid guid;
            if ((source is string) && Guid.TryParse((string)source, out guid)) return guid;
    
            return null;
        }
    }
}
