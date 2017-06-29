using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ObjectPopulatedTableData<T> : TableData
    {
        private PropertyInfo[] properties;

        public ObjectPopulatedTableData(IEnumerable<T> items)
        {
            var itemList = items.ToList();
            PopulateProperties();
            PopulateColumnNames(itemList);
            PopulateRows(itemList);
        }

        private void PopulateProperties()
        {
            properties = typeof(T).GetProperties();
        }

        private void PopulateColumnNames(IList<T> items)
        {
            ColumnNames = properties.Select(x => x.Name).ToList();
        }

        private void PopulateRows(IList<T> items)
        {
            Rows = items.Select(x => (IList<object>)(properties.Select(y => y.GetValue(x)).ToList())).ToList();
        }
    }
}
