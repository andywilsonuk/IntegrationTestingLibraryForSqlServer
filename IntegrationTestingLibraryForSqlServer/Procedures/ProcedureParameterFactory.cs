using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ProcedureParameterFactory
    {
        public IEnumerable<ProcedureParameter> FromRaw(IEnumerable<ProcedureParameterRaw> rawColumns)
        {
            foreach(var rawColumn in rawColumns)
            {
                ProcedureParameter column = FromDataType(new DataType(rawColumn.DataType), rawColumn.Name, rawColumn.Direction);

                var decimalColumn = column as DecimalProcedureParameter;
                if (decimalColumn != null)
                {
                    if (rawColumn.Size.HasValue) decimalColumn.Precision = (byte)rawColumn.Size.Value;
                    if (rawColumn.DecimalPlaces.HasValue) decimalColumn.Scale = rawColumn.DecimalPlaces.Value;
                }
                var sizeColumn = column as VariableSizeProcedureParameter;
                if (sizeColumn != null && rawColumn.Size.HasValue) sizeColumn.Size = rawColumn.Size.Value;

                yield return column;
            }
        }

        public ProcedureParameter FromDataType(DataType dataType, string name, ParameterDirection direction)
        {
            if (dataType.IsDecimal) return new DecimalProcedureParameter(name, direction);
            if (dataType.IsBinary) return new BinaryProcedureParameter(name, dataType.SqlType, direction);
            if (dataType.IsString) return new StringProcedureParameter(name, dataType.SqlType, direction);
            if (dataType.IsInteger) return new IntegerProcedureParameter(name, dataType.SqlType, direction);
            return new StandardProcedureParameter(name, dataType.SqlType, direction);
        }
    }
}
