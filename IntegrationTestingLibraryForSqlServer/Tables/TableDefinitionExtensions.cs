using System;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer
{
    public static class TableDefinitionExtensions
    {
        public static void CreateOrReplace(this TableDefinition definition, DatabaseActions database)
        {
            if (database == null) throw new ArgumentNullException("database");
            new TableActions(database.ConnectionString).CreateOrReplace(definition);
        }

        public static void Insert(this TableDefinition definition, DatabaseActions database, TableData tableData)
        {
            if (database == null) throw new ArgumentNullException("database");
            if (tableData == null) throw new ArgumentNullException("tableData");
            if (tableData.ColumnNames == null || !tableData.ColumnNames.Any())
                tableData = new CollectionPopulatedTableData(definition.Columns.Select(x => x.Name).ToList(), tableData.Rows);
            new TableActions(database.ConnectionString).Insert(definition.Name, tableData);
        }

        public static void VerifyMatch(this TableDefinition definition, DatabaseActions database)
        {
            if (database == null) throw new ArgumentNullException("database");
            var check = new TableCheck(database.ConnectionString);
            check.VerifyMatch(definition);
        }

        public static void VerifyMatchOrSubset(this TableDefinition definition, DatabaseActions database)
        {
            if (database == null) throw new ArgumentNullException("database");
            var check = new TableCheck(database.ConnectionString);
            check.VerifyMatchOrSubset(definition);
        }

        public static void CreateView(this TableDefinition definition, DatabaseActions database, string viewName)
        {
            CreateView(definition, database, new DatabaseObjectName(definition.Name.SchemaName, viewName));
        }

        public static void CreateView(this TableDefinition definition, DatabaseActions database, DatabaseObjectName viewName)
        {
            if (database == null) throw new ArgumentNullException("database");
            new TableActions(database.ConnectionString).CreateView(definition.Name, viewName);
        }
    }
}
