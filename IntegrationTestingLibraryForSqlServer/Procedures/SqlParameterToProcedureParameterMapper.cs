using System.Data.SqlClient;

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
            var sizeParameter = parameter as VariableSizeProcedureParameter;
            if (sizeParameter == null) return;
            sizeParameter.Size = sqlParameter.Size;
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
