using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer.TableDataComparison
{
    public class TableDataMatchSubsetColumnComparer : TableDataColumnComparer
    {
        protected IList<string> columnsX;
        protected IList<string> columnsY;

        public virtual void Initialise(TableData x, TableData y)
        {
            if (x.ColumnNames == null || !x.ColumnNames.Any()) throw new ArgumentNullException("Column names X cannot be null or empty");
            if (y.ColumnNames == null || !y.ColumnNames.Any()) throw new ArgumentNullException("Column names Y cannot be null or empty");
            this.columnsX = x.ColumnNames;
            this.columnsY = y.ColumnNames;
        }

        public virtual bool IsMatch()
        {
            return !this.columnsX.Except(this.columnsY, StringComparer.CurrentCultureIgnoreCase).Any();
        }

        public virtual IList<int> ColumnMappings
        {
            get
            {
                List<int> indexes = new List<int>();
                for (int i = 0; i < columnsX.Count; i++)
                {
                    for (int j = 0; j < columnsY.Count; j++)
                    {
                        if (columnsX[i].Equals(columnsY[j], StringComparison.CurrentCultureIgnoreCase))
                        {
                            indexes.Add(j);
                            break;
                        }
                    }
                }
                return indexes;
            }
        }
    }
}
