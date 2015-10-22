Feature: View
	View setup and populate functions

@db
Scenario: View setup and verify
	Given there is a test database
	When the table-backed view "test" is created
	| Name | Data Type | Size | Precision | Allow Nulls | 
	| Id   | int       |      |           | false       | 
	| Name | nvarchar  | 50   |           | true        | 
	Then the definition of view "test" should match
	| Name | Data Type | Size | Precision | Allow Nulls |
	| Id   | int       |      |           | false       |
	| Name | nvarchar  | 50   |           | true        |
	And the definition of view "test" should match SystemTables definition
	| Name | Data Type | Size | Precision | Allow Nulls |
	| Id   | int       |      |           | false       |
	| Name | nvarchar  | 50   |           | true        |

