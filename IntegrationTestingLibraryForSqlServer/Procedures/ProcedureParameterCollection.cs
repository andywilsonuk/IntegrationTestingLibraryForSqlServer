using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ProcedureParameterCollection : DistinctCollection<ProcedureParameter>
    {
        private static IEqualityComparer<ProcedureParameter> comparer = new NameEqualityComparer<ProcedureParameter>(GetName);

        public ProcedureParameterCollection() : base(comparer)
        {
        }

        public IEnumerable<ProcedureParameter> ExceptReturnValue
        {
            get { return Items.Where(x => x.Direction != ParameterDirection.ReturnValue); }
        }

        public void AddReturnValue()
        {
            if (Items.Any(x => x.Direction == ParameterDirection.ReturnValue)) return;
            Add(new IntegerProcedureParameter("returnValue", SqlDbType.Int, ParameterDirection.ReturnValue));
        }

        internal static string GetName(ProcedureParameter parameter) => parameter?.Name;
    }
}
