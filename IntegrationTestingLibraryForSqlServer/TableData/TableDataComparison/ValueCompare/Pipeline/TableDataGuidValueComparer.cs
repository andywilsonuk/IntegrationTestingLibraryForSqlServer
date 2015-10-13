using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataGuidValueComparer : TableDataValueComparerPipeElement
    {
        public void Process(TableDataValueComparerPipeElementArguments args)
        {
            if (args.MatchStatus != MatchedValueComparer.NotYetCompared) return;

            Guid? gX = this.ConvertToGuid(args.X);
            Guid? gY = this.ConvertToGuid(args.Y);

            if (!gX.HasValue || !gY.HasValue) return;

            args.MatchStatus = gX.Value.Equals(gY.Value) ? MatchedValueComparer.IsMatch : MatchedValueComparer.NoMatch;
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
