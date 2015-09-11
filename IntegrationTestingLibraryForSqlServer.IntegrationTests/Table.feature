Feature: Table
	Table setup and populate functions

@db
Scenario: Table setup and verify
	Given there is a test database
	When the table "test" is created
	| Name | Data Type | Size | Precision | Allow Nulls | Identity Seed |
	| Id   | int       |      |           | false       | 4             |
	| Name | nvarchar  | 50   |           | true        |               |
	Then the definition of table "test" should match
	| Name | Data Type | Size | Precision | Allow Nulls | Identity Seed |
	| Id   | int       |      |           | false       | 4             |
	| Name | nvarchar  | 50   |           | true        |               |

@db
Scenario: Table populate
	Given there is a test database
	And the table "test" is created
	| Name | Data Type | Size | Precision | Allow Nulls |
	| Id   | int       |      |           | false       |
	| Name | nvarchar  | 50   |           | true        |
	When table "test" is populated
	| Id | Name   |
	| 1  | First  |
	| 2  | Second |
	Then the table "test" should be populated with data
	| Id | Name   |
	| 1  | First  |
	| 2  | Second |

@db
Scenario: Table populate with identity
	Given there is a test database
	And the table "test" is created
	| Name | Data Type | Size | Precision | Allow Nulls | Identity Seed |
	| Id   | int       |      |           | false       | 5             |
	| Name | nvarchar  | 50   |           | true        |               |
	When table "test" is populated
	| Name   |
	| First  |
	| Second |
	Then the table "test" should be populated with data
	| Id | Name   |
	| 5  | First  |
	| 6  | Second |

@db
Scenario: Create a view based on a table
	Given there is a test database
	And the table "test" is created
	| Name | Data Type | Size | Precision | Allow Nulls |
	| Id   | int       |      |           | false       |
	| Name | nvarchar  | 50   |           | true        |
	And table "test" is populated
	| Id | Name   |
	| 1  | First  |
	| 2  | Second |
	When a view called "testview" of the table "test" is created
	Then the view "testview" filtered to id 2 should be populated with data
	| Id | Name   |
	| 2  | Second |
