#r "../IntegrationTestingLibraryForSqlServer/bin/debug/IntegrationTestingLibraryForSqlServer.dll"
using IntegrationTestingLibraryForSqlServer;
var database = new DatabaseActions(@"server=(localdb)\MSSQLLocalDB;database=Test;integrated security=True");
var tableChecker = new TableCheck(database.ConnectionString);
var table = tableChecker.GetDefinition(DatabaseObjectName.FromName("T1"));
string output = table.ToCSharp();