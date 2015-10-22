using System;
namespace IntegrationTestingLibraryForSqlServer
{
    public interface TableDefinitionInterrogationStrategy
    {
        TableDefinition GetTableDefinition(string tableName, string schemaName = Constants.DEFAULT_SCHEMA);
    }
}
