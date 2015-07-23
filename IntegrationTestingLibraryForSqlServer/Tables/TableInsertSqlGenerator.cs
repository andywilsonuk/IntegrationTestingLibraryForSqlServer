using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableInsertSqlGenerator
    {
        public string Sql(string name, IEnumerable<string> columnNames)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (columnNames == null || columnNames == Enumerable.Empty<string>()) throw new ArgumentNullException("columnNames");

            return string.Format(
                InsertTableFormat, 
                name,
                this.GetColumnNames(columnNames),
                this.BindingMarkers(columnNames.Count()));
        }

        public string Sql(string name, int columnCount)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (columnCount < 1) throw new ArgumentException("The column count must be greater than zero", "columnCount");

            return string.Format(
                InsertTableFormat,
                name,
                null,
                this.BindingMarkers(columnCount));
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
