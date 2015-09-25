using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataMatchSubsetRowComparer : TableDataOrdinalRowComparer
    {
        public override bool IsMatch()
        {
            return x.All(rowX => y.Any(rowY => this.IsRowEqual(rowX, rowY)));
        }

        private bool IsRowEqual(IList<object> rowX, IList<object> rowY)
        {
            for (int i = 0; i < rowX.Count; i++)
            {
                object valueX = rowX[i];
                object valueY = rowY[this.indexMappings[i]];

                if (!valueComparer.IsMatch(valueX, valueY)) return false;
            }

            return true;
        }
    }
}
