using System.Collections.Generic;
using System.Data;

namespace IntegrationTestingLibraryForSqlServer
{
    public static class ProcedureParameterCollectionExtensions
    {
        public static void AddFromRaw(this ProcedureParameterCollection parameters, IEnumerable<ProcedureParameterRaw> rawParameters)
        {
            var factory = new ProcedureParameterFactory();
            foreach (var column in factory.FromRaw(rawParameters))
                parameters.Add(column);
        }

        public static BinaryProcedureParameter AddBinary(this ProcedureParameterCollection parameters, string name, SqlDbType dataType)
        {
            var parameter = new BinaryProcedureParameter(name, dataType, ParameterDirection.InputOutput);
            parameters.Add(parameter);
            return parameter;
        }

        public static DecimalProcedureParameter AddDecimal(this ProcedureParameterCollection parameters, string name)
        {
            var parameter = new DecimalProcedureParameter(name, ParameterDirection.InputOutput);
            parameters.Add(parameter);
            return parameter;
        }

        public static IntegerProcedureParameter AddInteger(this ProcedureParameterCollection parameters, string name, SqlDbType dataType)
        {
            var parameter = new IntegerProcedureParameter(name, dataType, ParameterDirection.InputOutput);
            parameters.Add(parameter);
            return parameter;
        }

        public static StringProcedureParameter AddString(this ProcedureParameterCollection parameters, string name, SqlDbType dataType)
        {
            var parameter = new StringProcedureParameter(name, dataType, ParameterDirection.InputOutput);
            parameters.Add(parameter);
            return parameter;
        }
        public static StandardProcedureParameter AddStandard(this ProcedureParameterCollection parameters, string name, SqlDbType dataType)
        {
            var parameter = new StandardProcedureParameter(name, dataType, ParameterDirection.InputOutput);
            parameters.Add(parameter);
            return parameter;
        }
    }
}
