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
                ProcedureParameter column = FromSqlDbType(rawColumn.DataType, rawColumn.Name, rawColumn.Direction);

                var decimalColumn = column as DecimalProcedureParameter;
                if (decimalColumn != null)
                {
                    if (rawColumn.Size.HasValue) decimalColumn.Precision = (byte)rawColumn.Size.Value;
                    if (rawColumn.DecimalPlaces.HasValue) decimalColumn.Scale = rawColumn.DecimalPlaces.Value;
                }
                var sizeableColumn = column as SizeableProcedureParameter;
                if (sizeableColumn != null && rawColumn.Size.HasValue) sizeableColumn.Size = rawColumn.Size.Value;

                yield return column;
            }
        }

        public ProcedureParameter FromSqlDbType(string dataType, string name, ParameterDirection direction)
        {
            return FromSqlDbType(new DataTypeDefaults(dataType).SqlType, name, direction);
        }

        public ProcedureParameter FromSqlDbType(SqlDbType type, string name, ParameterDirection direction)
        {
            switch (type)
            {
                case SqlDbType.Decimal: return new DecimalProcedureParameter(name, direction);
                case SqlDbType.Binary:
                case SqlDbType.Char:
                case SqlDbType.VarBinary:
                case SqlDbType.VarChar:
                case SqlDbType.NChar:
                case SqlDbType.NVarChar: return new SizeableProcedureParameter(name, type, direction);
                default: return new ProcedureParameter(name, type, direction);
            }

            
        }
    }
}
