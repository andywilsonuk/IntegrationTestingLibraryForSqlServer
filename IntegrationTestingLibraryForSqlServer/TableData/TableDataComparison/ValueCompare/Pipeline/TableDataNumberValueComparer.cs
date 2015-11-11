using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataNumberValueComparer : TableDataValueComparerPipeElement
    {
        public void Process(TableDataValueComparerPipeElementArguments args)
        {
            if (args.MatchStatus != MatchedValueComparer.NotYetCompared) return;

            decimal? x = this.ConvertToDecimal(args.X);
            decimal? y = this.ConvertToDecimal(args.Y);

            if (!x.HasValue || !y.HasValue) return;

            args.MatchStatus = x.Value.Equals(y.Value) ? MatchedValueComparer.IsMatch : MatchedValueComparer.NoMatch;
        }

        private decimal? ConvertToDecimal(object source)
        {
            if (source is decimal) return (decimal)source;
            if (source is float || source is double || source is uint || source is int || source is long || source is ulong || source is sbyte || source is short || source is ushort) return Convert.ToDecimal(source);

            decimal number;
            if ((source is string) && decimal.TryParse((string)source, out number)) return number;
    
            return null;
        }
    }
}
