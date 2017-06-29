using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public interface TableDataColumnComparer
    {
        void Initialise(TableData x, TableData y);
        bool IsMatch();
        IList<int> ColumnMappings { get; }
    }
}
