using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    internal class SqlParameterToProcedureParameterMapper
    {
        private SqlParameter sqlParameter;
        private DataTypeDefaults dataTypeDefaults;

        public ProcedureParameter FromSqlParameter(SqlParameter parameter)
        {
            this.sqlParameter = parameter;
            this.dataTypeDefaults = new DataTypeDefaults(this.GetDataType());

            return new ProcedureParameter
            {
                Name = parameter.ParameterName,
                DataType = GetDataType(),
                Size = GetSize(),
                DecimalPlaces = GetDecimalPlaces(),
                Direction = parameter.Direction
            };
        }

        private SqlDbType GetDataType()
        {
            return sqlParameter.SqlDbType;
        }

        private int? GetSize()
        {
            DataTypeDefaults dataTypeDefaults = new DataTypeDefaults(sqlParameter.SqlDbType);
            if (dataTypeDefaults.IsSizeAllowed)
            {
                if (dataTypeDefaults.DataType == SqlDbType.Decimal)
                {
                    return (int?)sqlParameter.Precision;
                }
                else
                {
                    return sqlParameter.Size;
                }
            }
            return (int?)null;
        }

        private byte? GetDecimalPlaces()
        {
            DataTypeDefaults dataTypeDefaults = new DataTypeDefaults(sqlParameter.SqlDbType);
            if (dataTypeDefaults.AreDecimalPlacesAllowed)
            {
                var decimalPlaces = sqlParameter.Scale;
                return Convert.ToByte(decimalPlaces);
            }
            return (byte?)null;
        }
    }
}
