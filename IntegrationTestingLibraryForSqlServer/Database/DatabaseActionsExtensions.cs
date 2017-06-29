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
