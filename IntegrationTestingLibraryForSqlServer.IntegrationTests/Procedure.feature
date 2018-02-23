Feature: Procedure
	Procedure setup and tear down functions

@db
Scenario: Procedure setup and verify
	Given there is a test database
	When the procedure "test" is created with body "return 0"
	| Name     | Data Type | Size | Decimal Places | Direction |
	| Id       | int       |      |                | Input     |
	| Name     | nvarchar  | 50   |                | Input     |
	| Decimal1 | decimal   | 10   | 5              | Input     |
	| Decimal2 | decimal   | 10   |                | Input     |
	| Decimal3 | decimal   |      |                | Input     |
	Then the definition of procedure "test" should match
	| Name     | Data Type | Size | Decimal Places | Direction |
	| Id       | int       |      |                | Input     |
	| Name     | nvarchar  | 50   |                | Input     |
	| Decimal1 | decimal   | 10   | 5              | Input     |
	| Decimal2 | decimal   | 10   | 0              | Input     |
	| Decimal3 | decimal   | 18   | 0              | Input     |

@db
Scenario: Execute procedure and verify result
	Given there is a test database
	And the procedure "test" is created with body "return 5"
	| Name     | Data Type | Size | Decimal Places | Direction |
	| Id       | int       |      |                | Input     |
	| Name     | nvarchar  | 50   |                | Input     |
	| Decimal1 | decimal   | 10   | 5              | Input     |
	| Decimal2 | decimal   | 10   |                | Input     |
	| Decimal3 | decimal   |      |                | Input     |
	When the procedure "test" is executed
	| Name     | Data Type | Size | Decimal Places | Direction | Value |
	| Id       | int       |      |                | Input     | 1     |
	| Name     | nvarchar  | 50   |                | Input     | Test  |
	| Decimal1 | decimal   | 10   | 5              | Input     | 1.2   |
	| Decimal2 | decimal   | 10   | 0              | Input     | 1.2   |
	| Decimal3 | decimal   | 18   | 0              | Input     | 1.2   |
	Then the return value should be 5