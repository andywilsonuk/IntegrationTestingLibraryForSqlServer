using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public static class ProcedureParameterCollectionExtensions
    {
        public static void AddFromRaw(this ICollection<ProcedureParameter> parameters, IEnumerable<ProcedureParameterRaw> rawParameters)
        {
            var factory = new ProcedureParameterFactory();
            foreach (var column in factory.FromRaw(rawParameters))
                parameters.Add(column);
        }

        internal static BinaryProcedureParameter AddBinary(this ProcedureParameterCollection parameters, string name, SqlDbType dataType)
        {
            var parameter = new BinaryProcedureParameter(name, dataType, ParameterDirection.InputOutput);
            parameters.Add(parameter);
            return parameter;
        }
    }
}
