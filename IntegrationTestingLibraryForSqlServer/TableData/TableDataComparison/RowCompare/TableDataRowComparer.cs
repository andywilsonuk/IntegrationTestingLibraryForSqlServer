using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public interface TableDataRowComparer
    {
        void Initialise(IList<IList<object>> x, IList<IList<object>> y, IList<int> indexMappings, TableDataValueComparer valueComparer);
        bool IsMatch();
    }
}
