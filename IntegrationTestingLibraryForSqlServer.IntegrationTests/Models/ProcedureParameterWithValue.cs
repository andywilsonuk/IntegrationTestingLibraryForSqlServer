using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests
{
    class ProcedureParameterWithValue : ProcedureParameterRaw
    {
        public string Value { get; set; }

        public SqlParameter ToSqlParameter()
        {
            var parameter = new SqlParameter
            {
                ParameterName = Name.StartsWith("@") ? Name : "@" + Name,
                SqlDbType = new DataType(DataType).SqlType,
                Direction = Direction,
                Value = Value
            };

            if (!Size.HasValue) return parameter;
            if (parameter.SqlDbType != SqlDbType.Decimal)
            {
                parameter.Size = Size.Value;
                return parameter;
            }

            parameter.Precision = Convert.ToByte(Size.Value);
            if (DecimalPlaces.HasValue) parameter.Scale = Convert.ToByte(DecimalPlaces.Value);
            return parameter;
        }
    }
}
