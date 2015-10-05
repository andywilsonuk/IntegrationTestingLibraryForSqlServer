using IntegrationTestingLibraryForSqlServer.TableDataComparison;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableData
    {
        public TableData()
        {
            this.ColumnNames = new List<string>();
            this.Rows = new List<IList<object>>();
        }

        public IList<string> ColumnNames { get; set; }
        public IList<IList<object>> Rows { get; set; }

        public bool IsMatch(TableData other, TableDataComparer comparer)
        {
            return comparer.IsMatch(this, other);
        }

        public bool IsMatch(TableData other, TableDataComparers strategy)
        {
            return this.IsMatch(other, new TableDataComparerStrategyFactory().Comparer(strategy));
        }
    }
}
