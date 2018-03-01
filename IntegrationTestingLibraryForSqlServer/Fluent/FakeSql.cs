using System;

namespace IntegrationTestingLibraryForSqlServer
{
    public class FakeSql
    {
        public static FluentDatabase CreateDatabase(string connectionString)
        {
            var database = new DatabaseActions(connectionString);
            database.Create();
            return new FluentDatabase(database);
        }
    }
}