﻿Feature: Table
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
Scenario: Table setup and verify subset
	Given there is a test database
	When the table "test" is created
	| Name      | Data Type | Size | Precision | Allow Nulls | Identity Seed |
	| Id        | int       |      |           | false       | 4             |
	| Name      | nvarchar  | 50   |           | true        |               |
	| NewColumn | int       |      |           | true        |               |
	Then the definition of table "test" should contain
	| Name | Data Type | Size | Precision | Allow Nulls | Identity Seed |
	| Id   | int       |      |           | false       | 4             |
	| Name | nvarchar  | 50   |           | true        |               |

@db
Scenario: Table populate
	Given there is a test database
	And the table "test" is created
	| Name | Data Type        | Size | Precision | Allow Nulls |
	| Id   | int              |      |           | false       |
	| Name | nvarchar         | 50   |           | true        |
	| Uid  | uniqueidentifier |      |           | false       |
	When table "test" is populated
	| Id | Name   | Uid                                    |
	| 1  | First  | {15B42E6F-16D5-4DCB-B202-C9D24F20FD6A} |
	| 2  | Second | {71F0E5E6-D572-47E8-94A6-15FDA7E986A7} |
	Then the table "test" should be populated with data
	| Id | Name   | Uid                                    |
	| 1  | First  | 15B42E6F-16D5-4DCB-B202-C9D24F20FD6A   |
	| 2  | Second | {71F0E5E6-D572-47E8-94A6-15FDA7E986A7} |

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

@db
Scenario: Create a view based on a table in a schema
	Given there is a test database
	And the schema "testSchema" is created
	And the table "testTable" is created in the schema "testSchema"
	| Name | Data Type | Size | Precision | Allow Nulls |
	| Id   | int       |      |           | false       |
	| Name | nvarchar  | 50   |           | true        |
	When the view "testView" of the table "testTable" is created in the schema "testSchema"
	Then the table "testTable" exists in the schema "testSchema"

@db
Scenario: Schema creation
	Given there is a test database
	When the schema "testSchema" is created
	Then the schema "testSchema" exists

@db
Scenario: Schema and table creation
	Given there is a test database
	When the schema "testSchema" is created
	And the table "testTable" is created in schema "testSchema"
	| Name | Data Type | Size | Precision | Allow Nulls |
	| Id   | int       |      |           | false       |
	| Name | nvarchar  | 50   |           | true        |
	Then the table "testTable" exists in the schema "testSchema"
