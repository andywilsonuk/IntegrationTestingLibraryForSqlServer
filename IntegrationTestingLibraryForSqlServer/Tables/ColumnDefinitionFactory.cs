using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ColumnDefinitionFactory
    {
        public IEnumerable<ColumnDefinition> FromRaw(IEnumerable<ColumnDefinitionRaw> rawColumns)
        {
            foreach(var rawColumn in rawColumns)
            {
                ColumnDefinition column = FromSqlDbType(rawColumn.DataType, rawColumn.Name);

                var decimalColumn = column as DecimalColumnDefinition;
                if (decimalColumn != null)
                {
                    if (rawColumn.Size.HasValue) decimalColumn.Precision = (byte)rawColumn.Size.Value;
                    if (rawColumn.DecimalPlaces.HasValue) decimalColumn.Scale = rawColumn.DecimalPlaces.Value;
                }
                
                column.Name = rawColumn.Name;
                column.AllowNulls = rawColumn.AllowNulls;
                column.Size = rawColumn.Size;
                column.IdentitySeed = rawColumn.IdentitySeed;
                column.EnsureValid();
                yield return column;
            }
        }

        public ColumnDefinition FromSqlDbType(string dataType, string name)
        {
            if (dataType.ToLowerInvariant() == "numeric") dataType = "decimal";
            SqlDbType strongDataType = (SqlDbType)Enum.Parse(typeof(SqlDbType), dataType, true);
            return FromSqlDbType(strongDataType, name);
        }

        public ColumnDefinition FromSqlDbType(SqlDbType type, string name)
        {
            if (type == SqlDbType.Decimal) return new DecimalColumnDefinition(name);
            return new ColumnDefinition(name, type);
        }
    }
}
