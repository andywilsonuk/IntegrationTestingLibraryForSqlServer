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

            var xNormalised = args.X ?? DBNull.Value;
            var yNormalised = args.Y ?? DBNull.Value;

            if (xNormalised == DBNull.Value || yNormalised == DBNull.Value)
            {
                args.MatchStatus = xNormalised == yNormalised ? MatchedValueComparer.IsMatch : MatchedValueComparer.NoMatch;
            }
        }
    }
}
