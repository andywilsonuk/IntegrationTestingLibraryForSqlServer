using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableBackedViewCreateSqlGenerator
    {
        public string Sql(TableBackedViewDefinition definition)
        {
            if (definition == null) throw new ArgumentNullException("definition");
            return string.Format(CreateViewFormat, definition.Schema, definition.Name, definition.BackingTable);
        }

        private const string CreateViewFormat = "CREATE VIEW [{0}].[{1}] AS SELECT * FROM [{0}].[{2}]";
    }
}
