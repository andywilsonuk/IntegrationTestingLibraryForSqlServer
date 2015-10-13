using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataDefaultValueComparer : TableDataValueComparerPipeElement
    {
        public void Process(TableDataValueComparerPipeElementArguments args)
        {
            if (args.MatchStatus != MatchedValueComparer.NotYetCompared) return;

            var comparer = new TableDataCaseSensitiveStringValueComparer();

            args.MatchStatus = comparer.IsMatch(args.X, args.Y) ? MatchedValueComparer.IsMatch : MatchedValueComparer.NoMatch;
        }
    }
}
