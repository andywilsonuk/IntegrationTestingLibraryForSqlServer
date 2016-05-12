#r "../IntegrationTestingLibraryForSqlServer/bin/debug/IntegrationTestingLibraryForSqlServer.dll"
using IntegrationTestingLibraryForSqlServer;
string connectionString = @"server=(localdb)\MSSQLLocalDB;database=Test;integrated security=True";
string output = TableCodeBuilder.CSharpTableDefinition(DatabaseObjectName.FromName("T1"), connectionString);
Console.WriteLine(output);