Feature: Table
	Table setup and populate functions

@db
Scenario: Table setup and verify
	Given there is a test database
	When the table "test" is created
	| Name | Data Type | Size | Precision | Allow Nulls |
	| Id   | int       |      |           | false       |
	| Name | nvarchar  | 50   |           | true        |
	Then the definition of table "test" should match
	| Name | Data Type | Size | Precision | Allow Nulls |
	| Id   | int       |      |           | false       |
	| Name | nvarchar  | 50   |           | true        |

@db
Scenario: Table populate
	Given there is a test database
	When the table "test" is created
	| Name | Data Type | Size | Precision | Allow Nulls |
	| Id   | int       |      |           | false       |
	| Name | nvarchar  | 50   |           | true        |
	And table "test" is populated
	| Id | Name   |
	| 1  | First  |
	| 2  | Second |
	Then the table "test" should be populated with data
	| Id | Name   |
	| 1  | First  |
	| 2  | Second |