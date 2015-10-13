using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataNullValueComparer : TableDataValueComparerPipeElement
    {
        public void Process(TableDataValueComparerPipeElementArguments args)
        {
            if (args.MatchStatus != MatchedValueComparer.NotYetCompared) return;

            if (args.X == null)
            {
                args.MatchStatus = args.Y == null ? MatchedValueComparer.IsMatch : MatchedValueComparer.NoMatch;
                return;
            }
            if (args.Y == null)
            {
                args.MatchStatus = MatchedValueComparer.NoMatch;
                return;
            }
        }
    }
}
