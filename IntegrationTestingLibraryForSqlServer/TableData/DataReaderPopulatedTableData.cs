using System.Collections.Generic;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DataReaderPopulatedTableData : TableData
    {
        public DataReaderPopulatedTableData(IDataReader reader)
        {
            var columnNames = new List<string>();
            var rows = new List<IList<object>>();
            for (int i = 0; i < reader.FieldCount; i++) columnNames.Add(reader.GetName(i));
            ColumnNames = columnNames;

            while (reader.Read())
            {
                var row = new List<object>();
                for (int i = 0; i < reader.FieldCount; i++)
                    row.Add(reader.GetValue(i));
                rows.Add(row);
            }
            Rows = rows;
        }
    }
}
