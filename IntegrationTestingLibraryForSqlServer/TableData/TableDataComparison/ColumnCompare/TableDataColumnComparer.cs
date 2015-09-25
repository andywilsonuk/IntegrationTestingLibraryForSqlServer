using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public interface TableDataColumnComparer
    {
        void Initialise(TableData x, TableData y);
        bool IsMatch();
        IList<int> ColumnMappings { get; }
    }
}
