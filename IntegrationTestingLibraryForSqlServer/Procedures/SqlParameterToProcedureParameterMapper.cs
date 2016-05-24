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
        private ProcedureParameter parameter;
        private ProcedureParameterFactory factory = new ProcedureParameterFactory();

        public ProcedureParameter FromSqlParameter(SqlParameter sqlParameter)
        {
            this.sqlParameter = sqlParameter;
            DataType dataType = new DataType(this.sqlParameter.SqlDbType);
            parameter = factory.FromDataType(dataType, sqlParameter.ParameterName, sqlParameter.Direction);
            GetSize();
            GetPrecision();
            GetScale();
            return parameter;
        }

        private void GetSize()
        {
            var sizeableParameter = parameter as VariableSizeProcedureParameter;
            if (sizeableParameter == null) return;
            sizeableParameter.Size = sqlParameter.Size;
        }

        private void GetScale()
        {
            var decimalParameter = parameter as DecimalProcedureParameter;
            if (decimalParameter == null) return;
            decimalParameter.Scale = sqlParameter.Scale;
        }
        private void GetPrecision()
        {
            var decimalParameter = parameter as DecimalProcedureParameter;
            if (decimalParameter == null) return;
            decimalParameter.Precision = sqlParameter.Precision;
        }
    }
}
