using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class FluentDatabase
    {
        private DatabaseActions database;

        public FluentDatabase(DatabaseActions database)
        {
            this.database = database;
        }

        public FluentDatabase GrantUserAccess(SqlAccount account)
        {
            database.GrantUserAccess(account);
            return this;
        }

        public FluentDatabase GrantUserAccess(DomainAccount account)
        {
            database.GrantUserAccess(account);
            return this;
        }

        public FluentTable CreateTable(TableDefinition tableDefinition)
        {
            TableActions tableActions = new TableActions(database.ConnectionString);
            tableActions.Create(tableDefinition);
            return new FluentTable(tableDefinition.Name, database);
        }

        public void Drop()
        {
            database.Drop();
        }
    }
}
