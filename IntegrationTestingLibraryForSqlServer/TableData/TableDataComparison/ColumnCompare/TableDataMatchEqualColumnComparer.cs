using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataMatchEqualColumnComparer : TableDataMatchSubsetColumnComparer
    {
        public override bool IsMatch()
        {
            if (columnsX.Count != columnsY.Count) return false;
            return base.IsMatch();
        }
    }
}
