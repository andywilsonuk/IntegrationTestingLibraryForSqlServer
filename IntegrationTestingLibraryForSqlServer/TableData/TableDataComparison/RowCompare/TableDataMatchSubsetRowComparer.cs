using System.Collections.Generic;
using System.Linq;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataMatchSubsetRowComparer : TableDataOrdinalRowComparer
    {
        public override void Initialise(IList<IList<object>> x, IList<IList<object>> y, IList<int> indexMappings, TableDataValueComparer valueComparer)
        {
            this.x = x;
            this.y = y;
            this.indexMappings = indexMappings;
            this.valueComparer = valueComparer;
        }

        public override bool IsMatch()
        {
            return x.All(rowX => y.Any(rowY => IsRowEqual(rowX, rowY)));
        }

        private bool IsRowEqual(IList<object> rowX, IList<object> rowY)
        {
            for (int i = 0; i < rowX.Count; i++)
            {
                object valueX = rowX[i];
                object valueY = rowY[indexMappings[i]];

                if (!valueComparer.IsMatch(valueX, valueY)) return false;
            }

            return true;
        }
    }
}
