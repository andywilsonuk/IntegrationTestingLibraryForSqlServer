# IntegrationTestingLibraryForSqlServer
Provides helper functions for setting up and tearing down SQL Server database fakes for use in integration/acceptance testing.

Available on NuGet at https://www.nuget.org/packages/IntegrationTestingLibraryForSqlServer

Ideal for use with [SQL Server Local DB](http://blogs.msdn.com/b/sqlexpress/archive/2011/07/12/introducing-localdb-a-better-sql-express.aspx) which is deployed as part of Visual Studio but can also be installed on Integration Test servers. Specflow is fully supported and is our preferred method for creating integration tests, see further down this document for [Specflow integration best practices](#specflow-integration-best-practices).
```C#
using System.Data;
using IntegrationTestingLibraryForSqlServer;
```
There are some breaking changes in v2, see [Migrating from v1](#migrating-from-v1).
##Databases
###Setting up and tearing down databases
SQL Server databases can be created and dropped.
Windows Authentication access can be given to the user that the system under test will be running as (a website or web service for example).
```C#
var database = new DatabaseActions(connectionString);
database.CreateOrReplace();
database.GrantUserAccess(new DomainAccount(username));
database.Drop();
```
####Notes
* ```DomainAccount``` can include a Domain however if none is specified the domain of the account running the test is assumed
* SQL Authentication is not currently supported (but is planned)
* Schemas are supported, see [Schemas](#schemas) for usage

##Tables
###Creating tables
Tables can be created with the same structure as the 'real' table.
```C#
var definition = new TableDefinition(tableName);
definition.Columns.AddInteger("c1", SqlDbType.Int);
definition.CreateOrReplace(database);
```
####Notes
* A convenient way to create columns of the correct type is to use the ```ColumnDefinitionFactory``` factory although the resultant 
object will need to be casted to the specific type so that the extended properties can be changed.
* A create ```TableDefinition``` statement can be generated from a 'real' table using the [C# code generator](#code-generation).

####Standard columns
Most columns have no special properties. 
```C#
var column = new StandardColumnDefinition("c1", SqlDbType.DateTime);
```
or
```C#
var column = tableDefinition.Columns.AddStandard("c1", SqlDbType.DateTime);
```
####Integer columns
Columns that can be used as Identity columns have a ```SqlDbType``` of ```Int```, ```BigInt```, ```SmallInt```, ```TinyInt``` and provide an ```IdentitySeed``` column.
```C#
var column = new IntegerColumnDefinition("c1", SqlDbType.Int)
{
    IdentitySeed = 1
};
tableDefinition.Columns.Add(column);
```
or
```C#
var column = tableDefinition.Columns.AddInteger("c1", SqlDbType.Int);
column.IdentitySeed = 1;
```
####Decimal columns
Columns with a ```SqlDbType``` of ```Decimal``` (also shown in SQL Server as Numeric) can include ```Precision``` and ```Scale```. See [decimal and numeric (Transact-SQL)](https://msdn.microsoft.com/en-us/library/ms187746.aspx) for more details on usage.
```C#
var column = new DecimalColumnDefinition("c1")
{
    Precision = 18,
	Scale = 0
};
```
or
```C#
var column = tableDefinition.Columns.AddDecimal("c1");
```
####String columns
String-like columns, that is with a ```SqlDbType``` of ```Char```, ```VarChar```, ```NChar```, ```NVarChar``` can include ```Size```.
The property ```IsMaximumSize``` is a convenient way to set the column to the maximum size.
```C#
var column = new StringColumnDefinition("c1", SqlDbType.NVarChar)
{
    Size = 100
};
```
or
```C#
var column = tableDefinition.Columns.AddString("c1", SqlDbType.NVarChar);
```
####Binary columns
Variable length binary columns (```SqlDbType``` of ```Binary``` or ```VarBinary```) can include ```Size```.
The property ```IsMaximumSize``` is a convenient way to set the column to the maximum size.
```C#
var column = new BinaryColumnDefinition("c1", SqlDbType.Binary)
{
    Size = 1000
};
```
or
```C#
var column = tableDefinition.Columns.AddBinary("c1", SqlDbType.Binary);
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
tableActions.Insert(tableName, tableData, optionalSchemaName);
```
or, if you have a ```TableDefinition``` object:
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
Dependency tests can be created that will compare the expected table structure with that of the 'real' table 
to ensure that it has not changed structure (and therefore invalidating the primary test cases). ```VerifyMatch``` 
will throw an exception if the two structures don't match.

A create ```TableDefinition``` statement can be generated from a 'real' table using the [C# code generator](#code-generation).

```C#
var column1 = new IntegerColumnDefinition("c1", SqlDbType.Int);
var column2 = new StringColumnDefinition("c2", SqlDbType.NVarChar) { Size = 100 };
var definition = new TableDefinition(tableName, new[] { column1, column2 });
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

expected.VerifyMatch(actual, TableDataComparers.UnorderedRowNamedColumn);
```
There are a number of different matching strategies depending on how strict you want to be, how your system will access the real data, and what changes to the 'real' table are tolerable. the built-in comparers allow for combinations of:
* Columns: by ordinal, by name, by name as a subset of the returned columns
* Rows: by ordinal, by any sequence, by any sequence as a subset of the returned rows
* Values: currently the only matcher for values is by ```ToString()```

Custom comparers can be built using the classes in the TableComparision namespace.
####Notes
* If your system is executing a ```SELECT *``` you'll probably want the ordinal column comparer
* If your system can tolerate new columns to a table or view you can use the subset column comparer
* If you're inserting rows, you probably want your test to fail if new columns are added so match equals column comparer would be best
* If you're not selecting with an ```ORDER BY``` you'll probably want the unordered rows comparer as the order is not guaranteed subset rows is probably only useful for checking against a 'real' table with known values

###Verifying view structures
As for table structures, views can be tested to ensure that the 'real' view matches an expected structure. The method of retrieving the structure of a view differs from that of a table in that for a view the first row is selected and the resulting data reader is used for the comparision. ```VerifyMatch``` will throw an exception if the two structures don't match.
```C#
var column1 = new IntegerColumnDefinition("c1", SqlDbType.Int);
var column2 = new StringColumnDefinition("c2", SqlDbType.NVarChar) { Size = 100 };
var viewDefinition = new TableDefinition(viewName, new[] { column1, column2 });
var checker = new ViewCheck(this.database.ConnectionString);
checker.VerifyMatch(viewDefinition);
```
##Procedures
###Creating procedures
Procedures can be created with the same definition as the 'real' stored procedure but with predictable return values.
```C#
ProcedureDefinition definition = new ProcedureDefinition(procedureName)
{
    Body = @"set @p2 = 'ok'
             return 5"
};
definition.AddStandard("@p1", SqlDbType.Int).Direction = ParameterDirection.Input;
definition.AddString("@p2", SqlDbType.NVarChar);
definition.CreateOrReplace(database);
```
####Notes
* A convenient way to create parameters of the correct type is to use the ```ProcedureParameterFactory``` factory although the resultant 
object will need to be casted to the specific type so that the extended properties can be changed.
* The default direction when using AddStandard, AddString, etc is InputOutput.
```

####Standard parameters
Most parameters have no special properties. 
```C#
var parameter = new StandardProcedureParameter("c1", SqlDbType.DateTime, ParameterDirection.InputOutput);
procedureDefinition.Parameters.Add(parameter);
```
or
```C#
var parameter = procedureDefinition.Parameters.AddStandard("@c1", SqlDbType.DateTime);
```
####Integer parameters
Parameter for ```SqlDbType``` with values ```Int```, ```BigInt```, ```SmallInt``` and ```TinyInt```.
```C#
var parameter = new IntegerProcedureParameter("c1", SqlDbType.Int, ParameterDirection.InputOutput)
{
    IdentitySeed = 1
};
```
or
```C#
var parameter = procedureDefinition.Parameters.AddInteger("@c1", SqlDbType.Int);
parameter.IdentitySeed = 1;
```
####Decimal parameters
Parameters with a ```SqlDbType``` of ```Decimal``` (and also can shown in SQL Server as Numeric) can include ```Precision``` and ```Scale```. See [decimal and numeric (Transact-SQL)](https://msdn.microsoft.com/en-us/library/ms187746.aspx) for more details on usage.
```C#
var parameter = new DecimalProcedureParameter("c1", ParameterDirection.InputOutput)
{
    Precision = 18,
	Scale = 0
};
```
or
```C#
var parameter = procedureDefinition.Parameters.AddDecimal("@c1");
```
####String parameters
Variable size string-like parameters, that is with a ```SqlDbType``` of ```Char```, ```VarChar```, ```NChar```, ```NVarChar``` can include ```Size```.
The property ```IsMaximumSize``` is a convenient way to set the column to the maximum size.
```C#
var parameter = new StringProcedureParameter("c1", SqlDbType.NVarChar, ParameterDirection.InputOutput)
{
    Size = 100
};
```
or
```C#
var parameter = procedureDefinition.Parameters.AddString("@c1", SqlDbType.NVarChar);
```
####Binary parameters
Variable length binary parameters (```SqlDbType``` of ```Binary``` or ```VarBinary```) can include ```Size```.
The property ```IsMaximumSize``` is a convenient way to set the column to the maximum size.
```C#
var parameter = new BinaryProcedureParameter("c1", SqlDbType.Binary, ParameterDirection.InputOutput)
{
    Size = 1000
};
```
or
```C#
var parameter = procedureDefinition.Parameters.AddBinary("@c1", SqlDbType.Binary);
```
###Verifying stored procedure structures
Dependency tests can be created that will compare the expected stored procedure definition with that of the 'real' procedure to ensure that it has not changed definition (and therefore invalidating the primary test cases). ```VerifyMatch``` will throw an exception if the two definitions don't match.
```C#
var column1 = new StandardProcedureParameter("c1", SqlDbType.Int);
ProcedureDefinition definition = new ProcedureDefinition(procedureName, new[] { column1 });
definition.VerifyMatch(database);
```
##Schemas
###Creating a schema
```C#
var database = new DatabaseActions(connectionString);
database.CreateSchema("schemaName");
```
###Using schemas
Objects can be created in schemas other than dbo (the default schema) by creating a schema and then passing DatabaseObjectName instead of a string.

For example to create a table definition for the 'Test' schema (assuming it had already been created):
```C#
var definition = new TableDefinition(new DatabaseObjectName("Test", "Table1"));
```
An alternative would be to use the combined schema and object name:
```C#
var definition = new TableDefinition(DatabaseObjectName.FromName("Test.Table1"));
```
##Specflow integration best practices
[Specflow](http://www.specflow.org/) provides a behaviour-driven development structure ideally suited to integration/acceptance test as such this library has been designed to work well with it. There are helper extension methods included with Specflow which can be access by using the namespace ```TechTalk.SpecFlow.Assist```.
###Table creation
```Gherkin
Given the table "test" is created
| Name   | Data Type | Size | Decimal Places | Allow Nulls |
| Id     | int       |      |                | false       |
| Name   | nvarchar  | 50   |                | true        |
| Number | decimal   | 10   | 5              | true        |
```
```C#
[Given(@"the table ""(.*)"" is created")]
public void GivenTheTableIsCreated(string tableName, Table table)
{
    var definition = new TableDefinition(tableName);
    definition.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
    definition.CreateOrReplace(database);
}
```
###Table verification
```Gherkin
Then the definition of table "test" should match
| Name   | Data Type | Size | Decimal Places | Allow Nulls |
| Id     | int       |      |                | false       |
| Name   | nvarchar  | 50   |                | true        |
| Number | decimal   | 10   | 5              | true        |
```
```C#
[Then(@"the definition of table ""(.*)"" should match")]
public void ThenTheDefinitionOfTableShouldMatch(string tableName, Table table)
{
    var definition = new TableDefinition(tableName);
    definition.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
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
###View verification
```Gherkin
Then the definition of view "test" should match
| Name   | Data Type | Size | Decimal Places |
| Id     | int       |      |                |
| Name   | nvarchar  | 50   |                |
| Number | decimal   | 10   | 5              |
```
```C#
[Then(@"the definition of view ""(.*)"" should match")]
public void ThenTheDefinitionOfViewShouldMatch(string viewName, Table table)
{
    var definition = new TableDefinition(tableName);
    definition.Columns.AddFromRaw(table.CreateSet<ColumnDefinitionRaw>());
    var checker = new ViewCheck(this.database.ConnectionString);
    checker.VerifyMatch(definition);
}
```
###Procedure creation
```Gherkin
Given the procedure "test" is created with body "return 0"
| Name   | Data Type | Size | Decimal Places | Direction |
| Id     | int       |      |                | Input     |
| Name   | nvarchar  | 50   |                | Input     |
| Number | decimal   | 10   | 5              | Input     |
```
```C#
[Given(@"the procedure ""(.*)"" is created with body ""(.*)""")]
public void GivenTheProcedureIsCreatedWithBody(string procedureName, string body, Table table)
{
    ProcedureDefinition definition = new ProcedureDefinition(procedureName);
    definition.Parameters.AddFromRaw(table.CreateSet<ProcedureParameterRaw>());
	definition.Body = body;
    definition.CreateOrReplace(database);
}
```
###Procedure verification
```Gherkin
Then the definition of procedure "test" should match
| Name   | Data Type | Size | Decimal Places | Direction |
| Id     | int       |      |                | Input     |
| Name   | nvarchar  | 50   |                | Input     |
| Number | decimal   | 10   | 5              | Input     |
```
```C#
[Then(@"the definition of procedure ""(.*)"" should match")]
public void ThenTheDefinitionOfProcedureShouldMatch(string procedureName, Table table)
{
    ProcedureDefinition definition = new ProcedureDefinition(procedureName);
    definition.Parameters.AddFromRaw(table.CreateSet<ProcedureParameterRaw>());
    definition.VerifyMatch(database);
}
```
##Code generation
To make creating tests easier, the code snippet below can be adapted and pasted into the C# Interactive window to 
generate a ```TableDefinition``` code blob for an existing table. This code can then used as a 'fake' table or in a dependency 
test to [verify](#verifying-table-structures) the captured structure matches the current table structure.
```C#
#r "IntegrationTestingLibraryForSqlServer.dll"
using IntegrationTestingLibraryForSqlServer;
string connectionString = @"server=(localdb)\MSSQLLocalDB;database=Test;integrated security=True";
string tableName = "T1";
string output = TableCodeBuilder.CSharpTableDefinition(DatabaseObjectName.FromName(tableName), connectionString);
Console.WriteLine(output);
```
##Migrating from v1
There are a few breaking changes between version 1 and 2 specifically:

1. ```ColumnDefinition``` class can no longer be initialised; use ```ColumnDefinitionRaw``` instead and convert it (see [Table creation with Specflow](#table-creation) for example usage) or a specific concrete class instead (see [Creating tables](#creating-tables) for usage)
2. Likewise ```ProcedureParameter``` class can now be initialised by converting the ```ProcedureParameterRaw``` class (see [Procedure creation with Specflow](#procedure-creation) for example usage) or again use a specific concrete class (see [Creating procedures](#creating-procedures) for usage)
3. Database schemas are now better supported and standardised through the ```DatabaseObjectName``` class, existing overloads have been replaced to use this class (see [Schemas](#schemas) for usage)
4. To grant users access to the database the method ```GrantDomainUserAccess``` has been replaced with ```GrantUserAccess``` which accepts a new ```DomainAccount``` class (see [Setting up and tearing down databases](#setting-up-and-tearing-down-databases) for usage); it is expected that SQL authentication will be supported in the future
