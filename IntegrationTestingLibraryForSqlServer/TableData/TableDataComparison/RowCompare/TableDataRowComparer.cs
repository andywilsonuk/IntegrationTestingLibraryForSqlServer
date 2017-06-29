using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public interface TableDataRowComparer
    {
        void Initialise(IList<IList<object>> x, IList<IList<object>> y, IList<int> indexMappings, TableDataValueComparer valueComparer);
        bool IsMatch();
    }
}
