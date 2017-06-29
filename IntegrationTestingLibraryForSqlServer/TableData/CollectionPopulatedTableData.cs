using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer
{
    public class CollectionPopulatedTableData : TableData
    {
        public CollectionPopulatedTableData(IEnumerable<string> columnNames, IEnumerable<IEnumerable<string>> rows)
            : this(columnNames, rows.Select(x => x.Select(z => (object)z)))
        {
        }

        public CollectionPopulatedTableData(IEnumerable<string> columnNames, IEnumerable<IEnumerable<object>> rows)
        {
            ColumnNames = columnNames.ToList();
            Rows = rows.Select(x => (IList<object>)ReplaceNullWithDbNull(x).ToList()).ToList();
        }

        private IEnumerable<object> ReplaceNullWithDbNull(IEnumerable<object> row)
        {
            return row.Select(x => x == null || x.ToString() == "DBNull" ? null : x);
        }
    }
}
