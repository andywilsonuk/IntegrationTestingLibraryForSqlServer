﻿using System;
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
                sb.AppendFormat(@"    new {3}(""{0}""{1}) {{ {4} = {2}{5} }},",
                    column.Name,
                    SqlDbName(column),
                    column.AllowNulls.ToString().ToLowerInvariant(),
                    column.GetType().Name,
                    nameof(column.AllowNulls),
                    Extended(column));
                sb.AppendLine();
            }
            sb.Append("});");
            return sb.ToString();
        }

        private static string SqlDbName(ColumnDefinition column)
        {
            if (column is DecimalColumnDefinition) return null;
            return string.Format(", {0}.{1}", nameof(SqlDbType), column.DataType);
        }

        private static string Extended(ColumnDefinition column)
        {
            var sizeableColumn = column as SizeableColumnDefinition;
            if (sizeableColumn != null) return string.Format(", {0} = {1}", nameof(sizeableColumn.Size), sizeableColumn.Size);
            var integerColumn = column as IntegerColumnDefinition;
            if (integerColumn != null && integerColumn.IdentitySeed.HasValue) return string.Format(", {0} = {1}", nameof(integerColumn.IdentitySeed), integerColumn.IdentitySeed);
            var decimalColumn = column as DecimalColumnDefinition;
            if (decimalColumn != null) return string.Format(", {0} = {1}, {2} = {3}", nameof(decimalColumn.Precision), decimalColumn.Precision, nameof(decimalColumn.Scale), decimalColumn.Scale);
            return null;
        }
    }
}
