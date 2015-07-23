# IntegrationTestingLibraryForSqlServer
Provides helper functions for setting up and tearing down SQL Server database fakes for use in integration testing.

Available on NuGet at https://www.nuget.org/packages/IntegrationTestingLibraryForSqlServer

Ideal for use with [SQL Server Local DB](http://blogs.msdn.com/b/sqlexpress/archive/2011/07/12/introducing-localdb-a-better-sql-express.aspx) which is deployed as part of Visual Studio but can also be installed on Integration Test servers.
```C#
using System.Data;
using IntegrationTestingLibraryForSqlServer;
```
##Databases
###Setting up and tearing down databases
SQL Server databases can be created and dropped.
Windows Authentication access can be given to the user that the system under test will be running as (a website or web service for example).
```C#
var database = new DatabaseActions(connectionString);
database.CreateOrReplace();
database.GrantDomainUserAccess(Environment.UserDomainName, username);
database.Drop();
```

##Tables
###Creating tables
Tables can be created with the same structure as the 'real' table.
```C#
var column = new ColumnDefinition("c1", SqlDbType.Int);
var definition = new TableDefinition(tableName, new[] { column });
definition.CreateOrReplace(database);
```
###Populating tables with data
Tables can be loaded with initial data.
```C#
var tableActions = new TableActions(database.ConnectionString);
var rows = new List<List<object>>();
rows.Add(new List<object>() { 5, "name1" });
rows.Add(new List<object>() { 6, "name2" });

var tableData = new TableData
{
    ColumnNames = new[] { "c1", "c2" },
    Rows = rows
};
tableActions.Insert(tableName, tableData);
```
###Verifying table structures
Dependency tests can be created that will compare the expected table structure with that of the 'real' table to ensure that it has not changed structure (and therefore invalidating the primary test cases). VerifyEqual will throw an exception if the two structures don't match.
```C#
var column1 = new ColumnDefinition("c1", SqlDbType.Int);
var column2 = new ColumnDefinition("c2", SqlDbType.NVarChar);
var definition = new TableDefinition(tableName, new[] { column1, column2 });
definition.VerifyEqual(database);
```
##Procedures
###Creating procedures
Procedures can be created with the same definition as the 'real' stored procedure but with predictable return values.
```C#
List<ProcedureParameter> parameters = new List<ProcedureParameter>();
parameters.Add(new ProcedureParameter("@p1", SqlDbType.Int, ParameterDirection.Input));
parameters.Add(new ProcedureParameter("@p2", SqlDbType.NVarChar, ParameterDirection.InputOutput));
ProcedureDefinition definition = new ProcedureDefinition(procedureName, parameters)
{
    Body = @"set @p2 = 'ok'
             return 5"
};
definition.CreateOrReplace(database);
```
###Verifying stored procedure structures
Dependency tests can be created that will compare the expected stored procedure definition with that of the 'real' procedure to ensure that it has not changed definition (and therefore invalidating the primary test cases). VerifyEqual will throw an exception if the two definitions don't match.
```C#
var column1 = new ColumnDefinition("c1", SqlDbType.Int);
ProcedureDefinition definition = new ProcedureDefinition(procedureName, new[] { column1 });
definition.VerifyEqual(database);
```
