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
                var sizeableColumn = column as SizeableColumnDefinition;
                if (sizeableColumn != null && rawColumn.Size.HasValue) sizeableColumn.Size = rawColumn.Size.Value;

                yield return column;
            }
        }

        public ColumnDefinition FromSqlDbType(string dataType, string name)
        {
            return FromSqlDbType(new DataTypeDefaults(dataType).SqlType, name);
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
                case SqlDbType.Binary:
                case SqlDbType.Char:
                case SqlDbType.VarBinary:
                case SqlDbType.VarChar:
                case SqlDbType.NChar:
                case SqlDbType.NVarChar: return new SizeableColumnDefinition(name, type);
                default: return new ColumnDefinition(name, type);
            }

            
        }
    }
}
