# IntegrationTestingLibraryForSqlServer
Provides helper functions for setting up and tearing down SQL Server database fakes for use in integration/acceptance testing.

Available on NuGet at https://www.nuget.org/packages/IntegrationTestingLibraryForSqlServer

Ideal for use with [SQL Server Local DB](http://blogs.msdn.com/b/sqlexpress/archive/2011/07/12/introducing-localdb-a-better-sql-express.aspx) which is deployed as part of Visual Studio but can also be installed on Integration Test servers. Specflow is fully support and the preferred method for creating intgration tests, see further down this document for [Specflow integration best practices](#Specflow integration best practices).
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
Dependency tests can be created that will compare the expected table structure with that of the 'real' table to ensure that it has not changed structure (and therefore invalidating the primary test cases). ```VerifyEqual``` will throw an exception if the two structures don't match.
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
Dependency tests can be created that will compare the expected stored procedure definition with that of the 'real' procedure to ensure that it has not changed definition (and therefore invalidating the primary test cases). ```VerifyEqual``` will throw an exception if the two definitions don't match.
```C#
var column1 = new ColumnDefinition("c1", SqlDbType.Int);
ProcedureDefinition definition = new ProcedureDefinition(procedureName, new[] { column1 });
definition.VerifyEqual(database);
```
##Specflow integration best practices
[Specflow](http://www.specflow.org/) provides a behaviour-driven development structure ideally suited to integration/acceptance test as such this library has been designed to work well with it. There are helper extension methods included with Specflow which can be access by using the namespace ```TechTalk.SpecFlow.Assist```.
###Table creation
```Gherkin
Given the table "test" is created
| Name | Data Type | Size | Precision | Allow Nulls |
| Id   | int       |      |           | false       |
| Name | nvarchar  | 50   |           | true        |
```
```C#
[Given(@"the table ""(.*)"" is created")]
public void GivenTheTableIsCreated(string tableName, Table table)
{
    var definition = new TableDefinition(tableName, table.CreateSet<ColumnDefinition>());
    definition.CreateOrReplace(database);
}
```
###Table verification
```Gherkin
Then the definition of table "test" should match
| Name | Data Type | Size | Precision | Allow Nulls |
| Id   | int       |      |           | false       |
| Name | nvarchar  | 50   |           | true        |
```
```C#
[Then(@"the definition of table ""(.*)"" should match")]
public void ThenTheDefinitionOfTableShouldMatch(string tableName, Table table)
{
    var definition = new TableDefinition(tableName, table.CreateSet<ColumnDefinition>());
    definition.VerifyEqual(database);
}
```
###Table population
```Gherkin
And table "test" is populated
| Id | Name   |
| 1  | First  |
| 2  | Second |
```
```C#
[Given(@"table ""(.*)"" is populated")]
public void GivenTableIsPopulated(string tableName, Table table)
{
    var tableActions = new TableActions(database.ConnectionString);
    var tableData = new TableData
    {
        ColumnNames = table.Header,
        Rows = table.Rows.Select(x => x.Values)
    };
    tableActions.Insert(tableName, tableData);
}
```
###Procedure creation
```Gherkin
Given the procedure "test" is created with body "return 0"
| Name | Data Type | Size | Precision | Direction |
| Id   | int       |      |           | Input     |
| Name | nvarchar  | 50   |           | Input     |
```
```C#
[Given(@"the procedure ""(.*)"" is created with body ""(.*)""")]
public void GivenTheProcedureIsCreatedWithBody(string procedureName, string body, Table table)
{
    var definition = new ProcedureDefinition(procedureName, table.CreateSet<ProcedureParameter>())
    {
        Body = body
    };
    definition.CreateOrReplace(database);
}
```
###Procedure verification
```Gherkin
Then the definition of procedure "test" should match
| Name | Data Type | Size | Precision | Direction |
| Id   | int       |      |           | Input     |
| Name | nvarchar  | 50   |           | Input     |
```
```C#
[Then(@"the definition of procedure ""(.*)"" should match")]
public void ThenTheDefinitionOfProcedureShouldMatch(string procedureName, Table table)
{
    ProcedureDefinition definition = new ProcedureDefinition(procedureName, table.CreateSet<ProcedureParameter>());
    definition.VerifyEqual(database);
}
```
