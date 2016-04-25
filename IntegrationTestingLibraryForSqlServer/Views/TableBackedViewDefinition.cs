using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableBackedViewDefinition
    {
        public TableBackedViewDefinition(DatabaseObjectName name, DatabaseObjectName backingTable)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (backingTable == null) throw new ArgumentNullException(nameof(backingTable));

            Name = name;
            BackingTable = backingTable;
        }

        public DatabaseObjectName Name { get; private set; }
        public DatabaseObjectName BackingTable { get; private set; }
    }
}
