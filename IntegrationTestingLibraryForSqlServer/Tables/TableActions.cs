using System;
using System.Data.SqlClient;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableActions
    {
        private string connectionString;

        public TableActions(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(TableDefinition definition)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(new TableCreateSqlGenerator().Sql(definition));
            }
        }

        public void Drop(string name)
        {
            Drop(DatabaseObjectName.FromName(name));
        }

        public void Drop(DatabaseObjectName name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(dropTableCommand, name.Qualified);
            }
        }

        public void CreateOrReplace(TableDefinition definition)
        {
            Drop(definition.Name);
            Create(definition);
        }

        public void Insert(string tableName, TableData tableData)
        {
            Insert(DatabaseObjectName.FromName(tableName), tableData);
        }

        public void Insert(DatabaseObjectName tableName, TableData tableData)
        {
            if (tableName == null) throw new ArgumentNullException(nameof(tableName));
            if (tableData == null) throw new ArgumentNullException(nameof(tableData));

            bool hasColumnNames = tableData.ColumnNames != null && !tableData.ColumnNames.Equals(Enumerable.Empty<string>());
            var generator = new TableInsertSqlGenerator();
            using (var connection = new SqlConnection(this.connectionString))
            {
                foreach (var row in tableData.Rows)
                {
                    string command = hasColumnNames ? generator.Sql(tableName, tableData.ColumnNames) : generator.Sql(tableName, row.Count());

                    connection.ExecuteWithParameters(command, row);
                }
            }
        }

        public void CreateView(DatabaseObjectName tableName, DatabaseObjectName viewName)
        {
            if (tableName == null) throw new ArgumentNullException(nameof(tableName));
            if (viewName == null) throw new ArgumentNullException(nameof(viewName));

            var definition = new TableBackedViewDefinition(viewName, tableName);

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(new TableBackedViewCreateSqlGenerator().Sql(definition));
            }
        }

        private const string dropTableCommand = @"if exists (select * from sys.objects where object_id = object_id('{0}') and type = (N'U')) drop table {0}";
    }
}
