using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataCaseSensitiveStringValueComparer : TableDataValueComparer
    {
        public bool IsMatch(object x, object y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return string.Equals(x.ToString(), y.ToString());
        }
    }
}
