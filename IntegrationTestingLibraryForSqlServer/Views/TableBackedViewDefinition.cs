using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableBackedViewDefinition
    {
        public TableBackedViewDefinition(string name, string backingTable)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(backingTable)) throw new ArgumentNullException("backingTable");

            this.Name = name;
            this.BackingTable = backingTable;
        }

        public string Name { get; private set; }
        public string BackingTable { get; private set; }
    }
}
