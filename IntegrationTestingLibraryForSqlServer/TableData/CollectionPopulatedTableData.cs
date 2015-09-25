﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

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
            this.ColumnNames = columnNames.ToList();
            this.Rows = rows.Select(x => (IList<object>)x.ToList()).ToList();
        }
    }
}