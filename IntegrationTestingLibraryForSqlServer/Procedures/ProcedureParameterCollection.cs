using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    internal class ProcedureParameterCollection : DistinctCollection<ProcedureParameter>
    {
        private static IEqualityComparer<ProcedureParameter> comparer = new NameEqualityComparer<ProcedureParameter>(GetName);

        public ProcedureParameterCollection() : base(comparer)
        {
        }

        public IEnumerable<ProcedureParameter> ExcludingReturnValue
        {
            get { return Items.Where(x => x.Direction != ParameterDirection.ReturnValue); }
        }

        internal static string GetName(ProcedureParameter parameter) => parameter?.Name;
    }
}
