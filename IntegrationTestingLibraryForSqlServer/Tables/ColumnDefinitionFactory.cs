using System.Collections.Generic;

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

                if (column is DecimalColumnDefinition decimalColumn)
                {
                    if (rawColumn.Size.HasValue) decimalColumn.Precision = (byte)rawColumn.Size.Value;
                    if (rawColumn.DecimalPlaces.HasValue) decimalColumn.Scale = rawColumn.DecimalPlaces.Value;
                }
                if (column is IntegerColumnDefinition numberColumn)
                {
                    numberColumn.IdentitySeed = rawColumn.IdentitySeed;
                }
                if (column is VariableSizeColumnDefinition sizeColumn && rawColumn.Size.HasValue)
                    sizeColumn.Size = rawColumn.Size.Value;

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
