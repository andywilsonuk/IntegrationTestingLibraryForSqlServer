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
                var column = new ColumnDefinition
                {
                    Name = rawColumn.Name,
                    AllowNulls = rawColumn.AllowNulls,
                    Size = rawColumn.Size,
                    DecimalPlaces = rawColumn.DecimalPlaces,
                    DataType = GetDataType(rawColumn),
                    IdentitySeed = rawColumn.IdentitySeed,
                };
                column.EnsureValid();
                yield return column;
            }
        }

        private static SqlDbType GetDataType(ColumnDefinitionRaw rawColumn)
        {
            if (string.Equals(rawColumn.DataType, "numeric", StringComparison.CurrentCultureIgnoreCase)) return SqlDbType.Decimal;
            return (SqlDbType)Enum.Parse(typeof(SqlDbType), rawColumn.DataType, true);
        }
    }
}
