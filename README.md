# IntegrationTestingLibraryForSqlServer
Provides helper functions for setting up and tearing down SQL Server database fakes for use in integration/acceptance testing.

Available on NuGet at https://www.nuget.org/packages/IntegrationTestingLibraryForSqlServer

Ideal for use with [SQL Server Local DB](http://blogs.msdn.com/b/sqlexpress/archive/2011/07/12/introducing-localdb-a-better-sql-express.aspx) which is deployed as part of Visual Studio but can also be installed on Integration Test servers. Specflow is fully supported and is our preferred method for creating intgration tests, see further down this document for [Specflow integration best practices](#specflow-integration-best-practices).
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
##Schemas

###Creating a schema
```C#
var database = new DatabaseActions(connectionString);
database.CreateSchema("schemaName");
```

###Using schemas
Tables and views can be created in schemas other than dbo (the dault schema) by creating a schema and then using the table and view creations methods below.

If a TableDefinition object has it's Schema property set then any tables or views created using that TableDefinition object will be created in that schema. If the Schema property is not set then the default dbo schema will be used. If a non dbo schema is specified then it must already exist, eg by calling database.CreateSchema("schemaName") as above.

All operations on tables and views which take a table name or view name parameter also accept an optional schema parameter which defaults to “dbo” if not provided.

##Tables
###Creating tables
Tables can be created with the same structure as the 'real' table.
```C#
var column = new ColumnDefinition("c1", SqlDbType.Int);
var definition = new TableDefinition(tableName, new[] { column }, optionalSchemaName);
definition.CreateOrReplace(database);
```
An optional schema name may be provided in the TableDefinition constructor (or via the Schema property). If a schema is not provided then the table will be created in the dbo schema.
The schema must already exist before a table can be created using a TableDefinition with a schema other than dbo set.
Use DatabaseActions.CreateSchema to create a new schema (see Schemas).

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
tableActions.Insert(tableName, tableData, optionalSchemaName);
```
or, if you have a TableDefinition object:
```C#
tableDefinition.Insert(database, tableData);
```
###Creating views
Views can be created as a front to a single table; the single table can model the same structure as the 'real' view.
```C#
var tableActions = new TableActions(database.ConnectionString);
tableActions.CreateView("t1", "v1");
```
or, if you have a TableDefinition object:
```C#
tableDefinition.CreateView(database, "v1", optionalSchemaName);
```
###Verifying table structures
Dependency tests can be created that will compare the expected table structure with that of the 'real' table to ensure that it has not changed structure (and therefore invalidating the primary test cases). ```VerifyMatch``` will throw an exception if the two structures don't match.
```C#
var column1 = new ColumnDefinition("c1", SqlDbType.Int);
var column2 = new ColumnDefinition("c2", SqlDbType.NVarChar);
var definition = new TableDefinition(tableName, new[] { column1, column2 }, optionalSchemaName);
definition.VerifyMatch(database);
```
###Verifying table data
Some systems under test will write data (to a fake table) this can be verified by comparing the expected data with the results from a data reader:
```C#
var expected = new TableData();
expected.ColumnNames.Add("c1");
expected.Rows.Add(new List<object> { "v1" });

TableData actual;

using (SqlConnection connection = new SqlConnection(database.ConnectionString))
{
    using (SqlCommand command = connection.CreateCommand())
    {
        command.CommandText = "SELECT c1 FROM t1";
        connection.Open();

        using (var reader = command.ExecuteReader())
        {
            actual = new DataReaderPopulatedTableData(reader);
        }
    }
}

Assert.IsTrue(expected.IsMatch(actual, TableDataComparers.UnorderedRowNamedColumn));
```
There are a number of different matching strategies depending on how strict you want to be, how your system will access the real data, and what changes to the 'real' table are tolerable. the built-in comparers allow for combinations of:
Columns: by ordinal, by name, by name as a subset of the returned columns
Rows: by ordinal, by any sequence, by any sequence as a subset of the returned rows
Values: currently the only matcher for values is by ToString()

Custom comparers can be built using the classes in the TableComparision namespace.
####Notes
* If your system is executing a SELECT * you'll probably want the ordinal column comparer
* If your system can tolerate new columns to a table or view you can use the subset column comparer
* If you're inserting rows, you probably want your test to fail if new columns are added so match equals column comparer would be best
* If you're not selecting with an ORDER BY you'll probably want the unordered rows comparer as the order is not guaranteed subset rows is probably only useful for checking against a 'real' table with known values

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
Dependency tests can be created that will compare the expected stored procedure definition with that of the 'real' procedure to ensure that it has not changed definition (and therefore invalidating the primary test cases). ```VerifyMatch``` will throw an exception if the two definitions don't match.
```C#
var column1 = new ColumnDefinition("c1", SqlDbType.Int);
ProcedureDefinition definition = new ProcedureDefinition(procedureName, new[] { column1 });
definition.VerifyMatch(database);
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
    definition.VerifyMatch(database);
}
```
###Table population
```Gherkin
Given table "test" is populated
| Id | Name   |
| 1  | First  |
| 2  | Second |
```
```C#
[Given(@"table ""(.*)"" is populated")]
public void GivenTableIsPopulated(string tableName, Table table)
{
    var tableActions = new TableActions(database.ConnectionString);
    var tableData = new CollectionPopulatedTableData(table.Header, table.Rows.Select(x => x.Values));
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
    var definition = new ProcedureDefinition(procedureName, table.CreateSet<ProcedureParameter>());
    definition.VerifyMatch(database);
}
```
