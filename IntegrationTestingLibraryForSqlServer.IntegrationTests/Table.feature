Feature: Table
	Table setup and populate functions

@db
Scenario: Table setup and verify
	Given there is a test database
	When the table "test" is created
	| Name | Data Type | Size | Decimal Places | Allow Nulls | Identity Seed |
	| Id   | int       |      |                | false       | 4             |
	| Name | nvarchar  | 50   |                | true        |               |
	Then the definition of table "test" should match
	| Name | Data Type | Size | Decimal Places | Allow Nulls | Identity Seed |
	| Id   | int       |      |                | false       | 4             |
	| Name | nvarchar  | 50   |                | true        |               |

@db
Scenario: Table setup and verify with numeric column
	Given there is a test database
	When the table "test" is created with a numeric column
	| Name     | Data Type | Size | Decimal Places | Allow Nulls | Identity Seed |
	| Decimal1 | decimal   | 10   | 5              | true        |               |
	| Decimal2 | decimal   |      |                | true        |               |
	| Decimal3 | decimal   | 28   | 1              | false       |               |
	| Decimal4 | decimal   | 10   | 2              | true        |               |
	| Decimal5 | numeric   | 11   |                | true        |               |
	Then the definition of table "test" should match
	| Name     | Data Type | Size | Decimal Places | Allow Nulls | Identity Seed |
	| Decimal1 | decimal   | 10   | 5              | true        |               |
	| Decimal2 | decimal   | 18   | 0              | true        |               |
	| Decimal3 | numeric   | 28   | 1              | false       |               |
	| Decimal4 | decimal   | 10   | 2              | true        |               |
	| Decimal5 | decimal   | 11   | 0              | true        |               |

@db
Scenario: Table setup - decimal places set without size
	Given there is a test database
	Then an attempt to create the table "test" with an invalid definition should fail
	| Name     | Data Type | Size | Decimal Places |
	| Decimal4 | decimal   |      | 2              |

@db
Scenario: Table setup - decimal places greater than size
	Given there is a test database
	Then an attempt to create the table "test" with an invalid definition should fail
	| Name     | Data Type | Size | Decimal Places |
	| Decimal4 | decimal   | 10   | 11             |

@db
Scenario: Table setup and verify subset
	Given there is a test database
	When the table "test" is created
	| Name      | Data Type | Size | Decimal Places | Allow Nulls | Identity Seed |
	| Id        | int       |      |                | false       | 4             |
	| Name      | nvarchar  | 50   |                | true        |               |
	| NewColumn | int       |      |                | true        |               |
	Then the definition of table "test" should contain
	| Name | Data Type | Size | Decimal Places | Allow Nulls | Identity Seed |
	| Id   | int       |      |                | false       | 4             |
	| Name | nvarchar  | 50   |                | true        |               |

@db
Scenario: Table populate
	Given there is a test database
	And the table "test" is created
	| Name           | Data Type        | Size | Decimal Places | Allow Nulls |
	| Id             | int              |      |                | false       |
	| Name           | nvarchar         | 50   |                | false       |
	| Uid            | uniqueidentifier |      |                | false       |
	| Flag           | bit              |      |                | false       |
	| UpdateDate     | datetime         |      |                | false       |
	| FloatingNumber | decimal          | 6    | 2              | false       |
	| WholeNumber    | int              |      |                | false       |
	When table "test" is populated
	| Id | Name  | Uid                                    | Flag | UpdateDate | FloatingNumber | WholeNumber |
	| 1  | First | {15B42E6F-16D5-4DCB-B202-C9D24F20FD6A} | True | 2010-01-01 | 1234.50        | 02          |
	Then the table "test" should be populated with data
	| Id | Name  | Uid                                  | Flag | UpdateDate | FloatingNumber | WholeNumber |
	| 1  | First | 15B42E6F-16D5-4DCB-B202-C9D24F20FD6A | true | 2010-1-1   | 1234.5         | 2           |

@db
Scenario: Table populate with identity
	Given there is a test database
	And the table "test" is created
	| Name | Data Type | Size | Decimal Places | Allow Nulls | Identity Seed |
	| Id   | int       |      |                | false       | 5             |
	| Name | nvarchar  | 50   |                | true        |               |
	When table "test" is populated
	| Name   |
	| First  |
	| Second |
	Then the table "test" should be populated with data
	| Id | Name   |
	| 5  | First  |
	| 6  | Second |
	
@db
Scenario: Table populate with null
	Given there is a test database
	And the table "test" is created
	| Name | Data Type | Size | Decimal Places | Allow Nulls | Identity Seed |
	| Id   | int       |      |                | false       | 70            |
	| Date | DateTime  |      |                | true        |               |
	When table "test" is populated supporting Null values
	| Date       |
	| 2015-10-10 |
	| NULL       |
	Then the table "test" should be populated with Id and dates
	| Id | Date       |
	| 70 | 2015-10-10 |
	| 71 | NULL       |

@db
Scenario: Create a view based on a table
	Given there is a test database
	And the table "test" is created
	| Name | Data Type | Size | Decimal Places | Allow Nulls |
	| Id   | int       |      |                | false       |
	| Name | nvarchar  | 50   |                | true        |
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
	| Name | Data Type | Size | Decimal Places | Allow Nulls |
	| Id   | int       |      |                | false       |
	| Name | nvarchar  | 50   |                | true        |
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
	| Name | Data Type | Size | Decimal Places | Allow Nulls |
	| Id   | int       |      |                | false       |
	| Name | nvarchar  | 50   |                | true        |
	Then the table "testTable" exists in the schema "testSchema"

@db
Scenario: Table with numeric column
	Given there is a test database
	When the table "test" is created outside of the library
	| Name     | Data Type | Size | Decimal Places | Allow Nulls |
	| Decimal1 | numeric   | 10   | 5              | true        |
	Then the definition of table "test" should match
	| Name     | Data Type | Size | Decimal Places | Allow Nulls |
	| Decimal1 | decimal   | 10   | 5              | true        |
