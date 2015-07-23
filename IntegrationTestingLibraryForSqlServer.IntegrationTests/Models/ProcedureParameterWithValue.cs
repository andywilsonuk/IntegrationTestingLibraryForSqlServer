using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests
{
    class ProcedureParameterWithValue : ProcedureParameter
    {
        public ProcedureParameterWithValue()
        {
        }

        public ProcedureParameterWithValue(string name, SqlDbType dataType, ParameterDirection direction)
            : base(name, dataType, direction)
        {
        }

        public string Value { get; set; }

        public SqlParameter ToSqlParameter()
        {
            var parameter = new SqlParameter
            {
                ParameterName = this.QualifiedName,
                SqlDbType = this.DataType,
                Direction = this.Direction,
                Value = this.Value
            };
            if (this.Size.HasValue) parameter.Size = this.Size.Value;
            if (this.Precision.HasValue) parameter.Precision = this.Precision.Value;
            return parameter;
        }
    }
}
