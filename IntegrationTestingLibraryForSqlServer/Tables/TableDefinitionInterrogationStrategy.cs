namespace IntegrationTestingLibraryForSqlServer
{
    public interface TableDefinitionInterrogationStrategy
    {
        TableDefinition GetTableDefinition(DatabaseObjectName tableName);
    }
}
