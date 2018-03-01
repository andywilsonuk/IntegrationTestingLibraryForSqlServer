using System;
using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer
{
    public class FluentTable
    {
        private DatabaseActions database;
        private DatabaseObjectName name;

        public FluentTable(DatabaseObjectName name, DatabaseActions database)
        {
            this.name = name;
            this.database = database;
        }

        public FluentTable InsertRow(params object[] values)
        {
            var tableActions = new TableActions(database.ConnectionString);
            var tableData = new TableData() { Rows = new List<IList<object>> { values } };
            tableActions.Insert(name, tableData);
            return this;
        }
    }
}