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
database.GrantUserAccess(new DomainAccount(username));
database.Drop();
```
####General notes
* ```DomainAccount``` can include a Domain however if none is specified the domain of the account running the test is assumed
* SQL Authentication is not currently supported
```
##Schemas

###Creating a schema
```C#
var database = new DatabaseActions(connectionString);
database.CreateSchema("schemaName");
```

###Using schemas
Objects can be created in schemas other than dbo (the default schema) by creating a schema and then passing DatabaseObjectName instead of a string.
The most convenient way to create columns of the correct type is to use the ```ColumnDefinitionFactory``` factory

##Tables
###Creating tables
Tables can be created with the same structure as the 'real' table.
```C#
var column = new IntegerColumnDefinition("c1", SqlDbType.Int);
var definition = new TableDefinition(tableName, new[] { column });
definition.CreateOrReplace(database);
```

####Standard columns
Most columns have no special properties. 
```C#
var column = new StandardColumnDefinition("c1", SqlDbType.DateTime);
```

####Integer columns
Columns that can be used as Identity columns have a ```SqlDbType``` of ```Int```, ```BigInt```, ```SmallInt```, ```TinyInt``` and provide an ```IdentitySeed``` column.
```C#
var column = new IntegerColumnDefinition("c1", SqlDbType.Int)
{
    IdentitySeed = 1
};
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

####Sizeable columns
Variable size columns, that is with a ```SqlDbType``` of ```Binary```, ```Char```, ```VarBinary```, ```VarChar```, ```NChar```, ```NVarChar``` can include ```Size```.
The property ```IsMaximumSize``` is a convenient way to set the column to the maximum size.
```C#
var column = new SizeableColumnDefinition("c1", SqlDbType.NVarChar)
{
    Size = 100
};
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
Dependency tests can be created that will compare the expected table structure with that of the 'real' table to ensure that it has not changed structure (and therefore invalidating the primary test cases). ```VerifyMatch``` will throw an exception if the two structures don't match.
```C#
var column1 = new IntegerColumnDefinition("c1", SqlDbType.Int);
var column2 = new SizeableColumnDefinition("c2", SqlDbType.NVarChar) { Size = 100 };
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
var column2 = new SizeableColumnDefinition("c2", SqlDbType.NVarChar) { Size = 100 };
var viewDefinition = new TableDefinition(viewName, new[] { column1, column2 });
var checker = new ViewCheck(this.database.ConnectionString);
checker.VerifyMatch(viewDefinition);
```

##Procedures
###Creating procedures
Procedures can be created with the same definition as the 'real' stored procedure but with predictable return values.
The most convenient way to create parameters of the correct type is to use the ```ProcedureParameterFactory``` factory
```C#
List<ProcedureParameter> parameters = new List<ProcedureParameter>();
parameters.Add(new StandardProcedureParameter("@p1", SqlDbType.Int, ParameterDirection.Input));
parameters.Add(new SizeableProcedureParameter("@p2", SqlDbType.NVarChar, ParameterDirection.InputOutput));
ProcedureDefinition definition = new ProcedureDefinition(procedureName, parameters)
{
    Body = @"set @p2 = 'ok'
             return 5"
};
definition.CreateOrReplace(database);
```

####Standard parameters
Most parameters have no special properties. 
```C#
var parameter = new StandardProcedureParameter("c1", SqlDbType.DateTime, ParameterDirection.InputOutput);
```

####Decimal parameters
Parameters with a SqlDbType of Decimal (and also can shown in SQL Server as Numeric) can include Precision and Scale. See [decimal and numeric (Transact-SQL)](https://msdn.microsoft.com/en-us/library/ms187746.aspx) for more details on usage.
```C#
var parameter = new DecimalProcedureParameter("c1", ParameterDirection.InputOutput)
{
    Precision = 18,
	Scale = 0
};
```

####Sizeable parameters
Variable size parameters, that is with a SqlDbType of Binary, Char, VarBinary, VarChar, NChar, NVarChar can include Size.
The property ```IsMaximumSize``` is a convenient way to set the column to the maximum size.
```C#
var parameter = new SizeableProcedureParameter("c1", SqlDbType.NVarChar, ParameterDirection.InputOutput)
{
    Size = 100
};
```

###Verifying stored procedure structures
Dependency tests can be created that will compare the expected stored procedure definition with that of the 'real' procedure to ensure that it has not changed definition (and therefore invalidating the primary test cases). ```VerifyMatch``` will throw an exception if the two definitions don't match.
```C#
var column1 = new StandardProcedureParameter("c1", SqlDbType.Int);
ProcedureDefinition definition = new ProcedureDefinition(procedureName, new[] { column1 });
definition.VerifyMatch(database);
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
