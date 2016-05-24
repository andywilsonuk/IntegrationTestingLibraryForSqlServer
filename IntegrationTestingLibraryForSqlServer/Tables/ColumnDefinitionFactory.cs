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
                ColumnDefinition column = FromDataType(new DataType(rawColumn.DataType), rawColumn.Name);
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
                var sizeableColumn = column as VariableSizeColumnDefinition;
                if (sizeableColumn != null && rawColumn.Size.HasValue) sizeableColumn.Size = rawColumn.Size.Value;

                yield return column;
            }
        }

        public ColumnDefinition FromDataType(DataType dataType, string name)
        {
            if (dataType.IsDecimal) return new DecimalColumnDefinition(name);
            if (dataType.IsBinary) return new BinaryColumnDefinition(name, dataType.SqlType);
            if (dataType.IsString) return new StringColumnDefinition(name, dataType.SqlType);
            if (dataType.IsInteger) return new IntegerColumnDefinition(name, dataType.SqlType);
            return new StandardColumnDefinition(name, dataType.SqlType);
        }
    }
}
