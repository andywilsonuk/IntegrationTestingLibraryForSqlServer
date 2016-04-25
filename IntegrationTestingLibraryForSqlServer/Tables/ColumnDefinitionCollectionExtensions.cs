using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public static class ColumnDefinitionCollectionExtensions
    {
        public static void AddFromRaw(this ICollection<ColumnDefinition> columnDefinitions, IEnumerable<ColumnDefinitionRaw> rawColumns)
        {
            var factory = new ColumnDefinitionFactory();
            foreach (var column in factory.FromRaw(rawColumns))
                columnDefinitions.Add(column);
        }
    }
}
