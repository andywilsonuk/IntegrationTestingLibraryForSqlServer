Feature: Procedure
	Procedure setup and tear down functions

@db
Scenario: Procedure setup and verify
	Given there is a test database
	When the procedure "test" is created with body "return 0"
	| Name | Data Type | Size | Precision | Direction |
	| Id   | int       |      |           | Input     |
	| Name | nvarchar  | 50   |           | Input     |
	Then the definition of procedure "test" should match
	| Name | Data Type | Size | Precision | Direction |
	| Id   | int       |      |           | Input     |
	| Name | nvarchar  | 50   |           | Input     |

@db
Scenario: Execute procedure and verify result
	Given there is a test database
	And the procedure "test" is created with body "return 5"
	| Name | Data Type | Size | Precision | Direction |
	| Id   | int       |      |           | Input     |
	| Name | nvarchar  | 50   |           | Input     |
	When the procedure "test" is executed
	| Name | Data Type | Size | Precision | Direction | Value |
	| Id   | int       |      |           | Input     | 1     |
	| Name | nvarchar  | 50   |           | Input     | Test  |
	Then the return value should be 5