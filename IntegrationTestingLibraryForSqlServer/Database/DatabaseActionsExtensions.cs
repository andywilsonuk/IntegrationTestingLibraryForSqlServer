using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public static class DatabaseActionsExtensions
    {
        public static TableActions TableActions(this DatabaseActions databaseActions)
        {
            return new TableActions(databaseActions.ConnectionString);
        }
    }
}
