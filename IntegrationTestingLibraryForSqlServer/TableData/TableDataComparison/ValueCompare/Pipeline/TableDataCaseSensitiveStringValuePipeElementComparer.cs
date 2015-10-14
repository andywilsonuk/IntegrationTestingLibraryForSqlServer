using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataCaseSensitiveStringValuePipeElementComparer : TableDataValueComparerPipeElement
    {
        public void Process(TableDataValueComparerPipeElementArguments args)
        {
            if (args.MatchStatus != MatchedValueComparer.NotYetCompared) return;

            string x = args.X as string;
            string y = args.Y as string;

            if (x == null || y == null) return;

            args.MatchStatus = x.Equals(y) ? MatchedValueComparer.IsMatch : MatchedValueComparer.NoMatch;
        }
    }
}
