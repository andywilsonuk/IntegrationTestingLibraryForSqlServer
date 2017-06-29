using System;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataCompositeComparer : TableDataComparer
    {
        public TableDataCompositeComparer(TableDataColumnComparer columnComparer, TableDataRowComparer rowComparer, TableDataValueComparer valueComparer)
        {
            ColumnComparer = columnComparer;
            RowComparer = rowComparer;
            ValueComparer = valueComparer;
        }

        internal TableDataColumnComparer ColumnComparer { get; private set; }
        internal TableDataRowComparer RowComparer { get; private set; }
        internal TableDataValueComparer ValueComparer { get; private set; }

        public bool IsMatch(TableData x, TableData y)
        {
            if (x.Rows == null) throw new ArgumentNullException("x", "Rows in x cannot be null");
            if (y.Rows == null) throw new ArgumentNullException("y", "Rows in y cannot be null");

            ColumnComparer.Initialise(x, y);
            if (!ColumnComparer.IsMatch()) return false;

            RowComparer.Initialise(x.Rows, y.Rows, ColumnComparer.ColumnMappings, ValueComparer);

            return RowComparer.IsMatch();
        }
    }
}
