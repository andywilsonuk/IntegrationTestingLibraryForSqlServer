using System;
using System.Collections.Generic;
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
    }
}
