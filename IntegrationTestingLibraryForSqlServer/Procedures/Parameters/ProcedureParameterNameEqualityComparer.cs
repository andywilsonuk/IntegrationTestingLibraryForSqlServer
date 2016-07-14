using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    internal class ProcedureParameterNameEqualityComparer : IEqualityComparer<ProcedureParameter>
    {
        public bool Equals(ProcedureParameter x, ProcedureParameter y)
        {
            return string.Equals(x?.QualifiedName, y?.QualifiedName, StringComparison.CurrentCultureIgnoreCase);
        }

        public int GetHashCode(ProcedureParameter parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            return parameter.QualifiedName.GetHashCode();
        }
    }
}
