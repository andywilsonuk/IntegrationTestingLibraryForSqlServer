using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public static class TableCodeBuilder
    {
        public static string CSharpTableDefinition(DatabaseObjectName tableName, string connectionString)
        {
            var tableChecker = new TableCheck(connectionString);
            var table = tableChecker.GetDefinition(tableName);
            return table.ToCSharp();
        }

        internal static string ToCSharp(this TableDefinition table)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"var table = new {0}(new {1}(""{2}""), ",
                nameof(TableDefinition),
                nameof(DatabaseObjectName),
                table.Name.Qualified);
            sb.Append("new [] {");
            sb.AppendLine();
            foreach (var column in table.Columns)
            {
                sb.AppendFormat(@"    new {3} {{ {4} = ""{0}"", {5} = {7}.{1}, {6} = {2} }},",
                    column.Name,
                    column.DataType,
                    column.AllowNulls.ToString().ToLowerInvariant(),
                    nameof(ColumnDefinition),
                    nameof(column.Name),
                    nameof(column.DataType),
                    nameof(column.AllowNulls),
                    nameof(SqlDbType));
                sb.AppendLine();
            }
            sb.Append("});");
            return sb.ToString();
        }
    }
}
