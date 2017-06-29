using System.Collections.Generic;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ColumnDefinitionCollection : DistinctCollection<ColumnDefinition>
    {
        private static IEqualityComparer<ColumnDefinition> comparer = new NameEqualityComparer<ColumnDefinition>(GetName);

        public ColumnDefinitionCollection() : base(comparer)
        {
        }

        internal static string GetName(ColumnDefinition column) => column?.Name;
    }
}
