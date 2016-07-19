using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public static class ColumnDefinitionCollectionExtensions
    {
        public static void AddFromRaw(this ColumnDefinitionCollection columnDefinitions, IEnumerable<ColumnDefinitionRaw> rawColumns)
        {
            var factory = new ColumnDefinitionFactory();
            foreach (var column in factory.FromRaw(rawColumns))
                columnDefinitions.Add(column);
        }
        public static BinaryColumnDefinition AddBinary(this ColumnDefinitionCollection columns, string name, SqlDbType dataType)
        {
            var column = new BinaryColumnDefinition(name, dataType);
            columns.Add(column);
            return column;
        }

        public static DecimalColumnDefinition AddDecimal(this ColumnDefinitionCollection columns, string name)
        {
            var column = new DecimalColumnDefinition(name);
            columns.Add(column);
            return column;
        }

        public static IntegerColumnDefinition AddInteger(this ColumnDefinitionCollection columns, string name, SqlDbType dataType)
        {
            var column = new IntegerColumnDefinition(name, dataType);
            columns.Add(column);
            return column;
        }

        public static StringColumnDefinition AddString(this ColumnDefinitionCollection columns, string name, SqlDbType dataType)
        {
            var column = new StringColumnDefinition(name, dataType);
            columns.Add(column);
            return column;
        }
        public static StandardColumnDefinition AddStandard(this ColumnDefinitionCollection columns, string name, SqlDbType dataType)
        {
            var column = new StandardColumnDefinition(name, dataType);
            columns.Add(column);
            return column;
        }
    }
}
