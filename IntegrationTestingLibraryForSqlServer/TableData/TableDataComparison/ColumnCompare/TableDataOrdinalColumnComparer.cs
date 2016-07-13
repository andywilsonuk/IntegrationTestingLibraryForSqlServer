using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataOrdinalColumnComparer : TableDataColumnComparer
    {
        private TableData x;
        private TableData y;

        public virtual void Initialise(TableData x, TableData y)
        {
            this.x = x;
            this.y = y;
            if (this.x.Rows.Count == 0 && this.y.Rows.Count != 0) throw new ArgumentException("Cannot determine columns for comparison because x has no rows but y does.", "x");
            if (this.x.Rows.Count != 0 && this.x.Rows[0].Count == 0) throw new ArgumentException("Cannot determine columns for comparison because row 0 in x has no values", "x");
        }

        public virtual bool IsMatch()
        {
            if (x.Rows.Count == 0 && y.Rows.Count == 0) return true;
            if (x.Rows.Count != 0 && y.Rows.Count == 0) return false;
            return x.Rows[0].Count == y.Rows[0].Count;
        }

        public virtual IList<int> ColumnMappings
        {
            
            get
            {
                if (x.Rows.Count == 0) return new List<int>();
                return Enumerable.Range(0, x.Rows[0].Count).ToList();
            }
        }
    }
}
