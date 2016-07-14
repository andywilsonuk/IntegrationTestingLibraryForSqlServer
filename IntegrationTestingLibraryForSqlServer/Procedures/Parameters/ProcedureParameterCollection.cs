using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    internal class ProcedureParameterCollection : Collection<ProcedureParameter>
    {
        private ProcedureParameterNameEqualityComparer comparer = new ProcedureParameterNameEqualityComparer();

        public ProcedureParameterCollection() : base()
        {
        }

        public ProcedureParameterCollection(IList<ProcedureParameter> list) : base(list)
        {
        }

        protected override void InsertItem(int index, ProcedureParameter item)
        {
            if (Items.Any(x => comparer.Equals(x, item)))
                throw new ArgumentException($"A parameter with the same name '{item.QualifiedName}' already exists", nameof(item));
            base.InsertItem(index, item);
        }

        public IEnumerable<ProcedureParameter> ExcludingReturnValue
        {
            get { return Items.Where(x => x.Direction != ParameterDirection.ReturnValue); }
        }
    }
}
