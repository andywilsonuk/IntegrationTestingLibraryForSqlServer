using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataDateTimeValueComparer : TableDataValueComparerPipeElement
    {
        public void Process(TableDataValueComparerPipeElementArguments args)
        {
            if (args.MatchStatus != MatchedValueComparer.NotYetCompared) return;

            DateTime? x = this.ConvertToDateTime(args.X);
            DateTime? y = this.ConvertToDateTime(args.Y);

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
