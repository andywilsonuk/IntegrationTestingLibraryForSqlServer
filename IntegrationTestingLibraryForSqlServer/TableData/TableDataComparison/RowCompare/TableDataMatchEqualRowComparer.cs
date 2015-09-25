using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataMatchEqualRowComparer : TableDataMatchSubsetRowComparer
    {
        public override bool IsMatch()
        { 
            if (x.Count != y.Count) return false;
            return base.IsMatch();
        }
    }
}
