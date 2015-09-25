using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataCompositeComparer : TableDataComparer
    {
        public TableDataCompositeComparer(TableDataColumnComparer columnComparer, TableDataRowComparer rowComparer, TableDataValueComparer valueComparer)
        {
            this.ColumnComparer = columnComparer;
            this.RowComparer = rowComparer;
            this.ValueComparer = valueComparer;
        }

        internal TableDataColumnComparer ColumnComparer { get; private set; }
        internal TableDataRowComparer RowComparer { get; private set; }
        internal TableDataValueComparer ValueComparer { get; private set; }

        public bool IsMatch(TableData x, TableData y)
        {
            if (x.Rows == null) throw new ArgumentNullException("x", "Rows in x cannot be null");
            if (y.Rows == null) throw new ArgumentNullException("y", "Rows in y cannot be null");

            this.ColumnComparer.Initialise(x, y);
            if (!this.ColumnComparer.IsMatch()) return false;

            this.RowComparer.Initialise(x.Rows, y.Rows, this.ColumnComparer.ColumnMappings, this.ValueComparer);

            return this.RowComparer.IsMatch();
        }
    }
}
