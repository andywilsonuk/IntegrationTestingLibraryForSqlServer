using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public static class TableDefinitionExtensions
    {
        public static void CreateOrReplace(this TableDefinition definition, DatabaseActions database)
        {
            new TableActions(database.ConnectionString).CreateOrReplace(definition);
        }

        public static void Insert(this TableDefinition definition, DatabaseActions database, TableData tableData)
        {
            tableData.ColumnNames = definition.Columns.Select(x => x.Name);
            new TableActions(database.ConnectionString).Insert(definition.Name, tableData);
        }

        public static void VerifyEqual(this TableDefinition definition, DatabaseActions database)
        {
            var check = new TableCheck(database.ConnectionString);
            check.VerifyMatch(definition);
        }
    }
}
