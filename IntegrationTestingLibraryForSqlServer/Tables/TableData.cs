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
            this.ColumnNames = new Collection<string>();
            this.Rows = new Collection<IEnumerable<object>>();
        }

        public IEnumerable<string> ColumnNames { get; set; }
        public IEnumerable<IEnumerable<object>> Rows { get; set; }
    }
}
