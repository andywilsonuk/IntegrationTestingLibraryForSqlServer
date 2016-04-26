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
                column.AllowNulls = rawColumn.AllowNulls;

                var decimalColumn = column as DecimalColumnDefinition;
                if (decimalColumn != null)
                {
                    if (rawColumn.Size.HasValue) decimalColumn.Precision = (byte)rawColumn.Size.Value;
                    if (rawColumn.DecimalPlaces.HasValue) decimalColumn.Scale = rawColumn.DecimalPlaces.Value;
                }
                var numberColumn = column as IntegerColumnDefinition;
                if (numberColumn != null)
                {
                    numberColumn.IdentitySeed = rawColumn.IdentitySeed;
                }

                column.Size = rawColumn.Size;
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
            switch (type)
            {
                case SqlDbType.Decimal: return new DecimalColumnDefinition(name);
                case SqlDbType.Int:
                case SqlDbType.BigInt:
                case SqlDbType.SmallInt:
                case SqlDbType.TinyInt: return new IntegerColumnDefinition(name, type);

                default: return new ColumnDefinition(name, type);
            }

            
        }
    }
}
