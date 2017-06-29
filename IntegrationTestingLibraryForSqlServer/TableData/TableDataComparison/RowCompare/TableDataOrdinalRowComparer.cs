using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataOrdinalRowComparer : TableDataRowComparer
    {
        protected IList<IList<object>> x;
        protected IList<IList<object>> y;
        protected IList<int> indexMappings;
        protected TableDataValueComparer valueComparer;

        public virtual void Initialise(IList<IList<object>> x, IList<IList<object>> y, IList<int> indexMappings, TableDataValueComparer valueComparer)
        {
            if (x.Any(rowX => rowX.Count != indexMappings.Count)) throw new ArgumentException("One of the rows in x has a missing value.", "x");
            if (y.Any(rowY => rowY.Count != indexMappings.Count)) throw new ArgumentException("One of the rows in y has a missing value.", "y");
            this.x = x;
            this.y = y;
            this.indexMappings = indexMappings;
            this.valueComparer = valueComparer;
        }

        public virtual bool IsMatch()
        {
            if (x.Count != y.Count) return false;

            for (int rowIndex = 0; rowIndex < x.Count; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < indexMappings.Count; columnIndex++)
                {
                    object valueX = x[rowIndex][columnIndex];
                    object valueY = y[rowIndex][indexMappings[columnIndex]];

                    if (!valueComparer.IsMatch(valueX, valueY)) return false;
                }
            }
            return true;
        }
    }
}
