using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableInsertSqlGenerator
    {
        public string Sql(DatabaseObjectName name, IEnumerable<string> columnNames)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (columnNames == null || columnNames == Enumerable.Empty<string>()) throw new ArgumentNullException("columnNames");

            return string.Format(
                InsertTableFormat,
                name,
                GetColumnNames(columnNames),
                BindingMarkers(columnNames.Count()));
        }

        public string Sql(DatabaseObjectName name, int columnCount)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (columnCount < 1) throw new ArgumentException("The column count must be greater than zero", "columnCount");

            return string.Format(
                InsertTableFormat,
                name,
                null,
                BindingMarkers(columnCount));
        }

        private string GetColumnNames(IEnumerable<string> columns)
        {
            return string.Format(" ({0})", string.Join(",", columns));
        }

        private string BindingMarkers(int columnCount)
        {
            return string.Join(",", Enumerable.Range(0, columnCount).Select(x => "@" + x));
        }

        private const string InsertTableFormat = "INSERT INTO {0}{1} SELECT {2}";
    }
}
