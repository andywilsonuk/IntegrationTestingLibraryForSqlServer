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
            if (this.columnsX.Count != this.columnsY.Count) return false;
            return base.IsMatch();
        }
    }
}
