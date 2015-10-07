using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public class TableBackedViewDefinition
    {
        public TableBackedViewDefinition(string name, string backingTable, string schema = Constants.DEFAULT_SCHEMA)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(backingTable)) throw new ArgumentNullException("backingTable");
            if (string.IsNullOrWhiteSpace(schema)) throw new ArgumentNullException("schema");

            this.Name = name;
            this.BackingTable = backingTable;
            this.Schema = schema;
        }

        public string Name { get; private set; }
        public string BackingTable { get; private set; }
        public string Schema { get; private set; }
    }
}
