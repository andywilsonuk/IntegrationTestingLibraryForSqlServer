using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class DataReaderPopulatedTableData : TableData
    {
        public DataReaderPopulatedTableData(IDataReader reader)
        {
            var columnNames = new List<string>();
            var rows = new List<IList<object>>();
            for (int i = 0; i < reader.FieldCount; i++) columnNames.Add(reader.GetName(i));
            this.ColumnNames = columnNames;

            while (reader.Read())
            {
                var row = new List<object>();
                for (int i = 0; i < reader.FieldCount; i++)
                    row.Add(reader.GetValue(i));
                rows.Add(row);
            }
            this.Rows = rows;
        }
    }
}
