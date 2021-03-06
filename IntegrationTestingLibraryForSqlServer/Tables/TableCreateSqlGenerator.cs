﻿using System;
using System.Data;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableCreateSqlGenerator
    {
        public string Sql(TableDefinition definition)
        {
            if (definition == null) throw new ArgumentNullException(nameof(definition));
            if (definition.Columns.Count == 0) throw new ArgumentException("The Table Definition must have at least one column", nameof(definition));
            return string.Format(CreateTableFormat, definition.Name.Qualified, CreateCommaSeparatedColumns(definition));
        }

        private string CreateCommaSeparatedColumns(TableDefinition definition)
        {
            return string.Join(",", definition.Columns.Select(x => GetFormattedColumnLine(x)));
        }

        private string GetFormattedColumnLine(ColumnDefinition column)
        {
            return string.Format(
                "[{0}] {1}{2}{3}",
                column.Name,
                GetFormattedDataType(column),
                GetFormattedIdentity(column),
                GetFormattedNullable(column));
        }

        private string GetFormattedDataType(ColumnDefinition column)
        {
            var sizeColumn = column as VariableSizeColumnDefinition;
            if (sizeColumn != null)
            {
                string size = sizeColumn.IsMaximumSize ? "max" : sizeColumn.Size.ToString();
                return string.Format("{0}({1})", column.DataType, size);
            }
            var decimalColumn = column as DecimalColumnDefinition;
            if (decimalColumn != null)
            {
                return string.Format("{0}({1},{2})", column.DataType, decimalColumn.Precision, decimalColumn.Scale);
            }
            return column.DataType.ToString();
        }

        private string GetFormattedIdentity(ColumnDefinition column)
        {
            var identityColumn = column as IntegerColumnDefinition;
            if (identityColumn == null || !identityColumn.IdentitySeed.HasValue) return " ";
            return string.Format(" IDENTITY({0},1) ", identityColumn.IdentitySeed.Value);
        }

        private string GetFormattedNullable(ColumnDefinition column)
        {
            return column.AllowNulls ? "NULL" : "NOT NULL";
        }

        private const string CreateTableFormat = "CREATE TABLE {0} ({1})";
    }
}
