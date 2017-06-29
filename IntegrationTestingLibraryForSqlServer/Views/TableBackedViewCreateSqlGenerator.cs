using System;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableBackedViewCreateSqlGenerator
    {
        public string Sql(TableBackedViewDefinition definition)
        {
            if (definition == null) throw new ArgumentNullException("definition");
            return string.Format(CreateViewFormat, definition.Name.Qualified, definition.BackingTable.Qualified);
        }

        private const string CreateViewFormat = "CREATE VIEW {0} AS SELECT * FROM {1}";
    }
}
