Feature: SqlServer
	Ensure we understand how SQL Server works

@db
Scenario: Default sizes
	Given there is a test database
	When the table "test" is created
	| Name | Data Type        | Size | Decimal Places | Allow Nulls |
	| C01  | BigInt           |      |                | true        |
	| C02  | Binary           |      |                | true        |
	| C03  | Bit              |      |                | true        |
	| C04  | Char             |      |                | true        |
	| C05  | DateTime         |      |                | true        |
	| C06  | Decimal          |      |                | true        |
	| C07  | Float            |      |                | true        |
	| C08  | Image            |      |                | true        |
	| C09  | Int              |      |                | true        |
	| C10  | Money            |      |                | true        |
	| C11  | NChar            |      |                | true        |
	| C12  | NText            |      |                | true        |
	| C13  | NVarChar         |      |                | true        |
	| C14  | Real             |      |                | true        |
	| C15  | UniqueIdentifier |      |                | true        |
	| C16  | SmallDateTime    |      |                | true        |
	| C17  | SmallInt         |      |                | true        |
	| C18  | SmallMoney       |      |                | true        |
	| C19  | Text             |      |                | true        |
	| C20  | Timestamp        |      |                | true        |
	| C21  | TinyInt          |      |                | true        |
	| C22  | VarBinary        |      |                | true        |
	| C23  | VarChar          |      |                | true        |
	| C25  | Xml              |      |                | true        |
	| C28  | Date             |      |                | true        |
	| C29  | Time             |      |                | true        |
	| C30  | DateTime2        |      |                | true        |
	| C31  | DateTimeOffset   |      |                | true        |
	Then the definition of table "test" should match
	| Name | Data Type        | Size | Decimal Places | Allow Nulls |
	| C01  | BigInt           |      |                | true        |
	| C02  | Binary           |      |                | true        |
	| C03  | Bit              |      |                | true        |
	| C04  | Char             |      |                | true        |
	| C05  | DateTime         |      |                | true        |
	| C06  | Decimal          |      |                | true        |
	| C07  | Float            |      |                | true        |
	| C08  | Image            |      |                | true        |
	| C09  | Int              |      |                | true        |
	| C10  | Money            |      |                | true        |
	| C11  | NChar            |      |                | true        |
	| C12  | NText            |      |                | true        |
	| C13  | NVarChar         |      |                | true        |
	| C14  | Real             |      |                | true        |
	| C15  | UniqueIdentifier |      |                | true        |
	| C16  | SmallDateTime    |      |                | true        |
	| C17  | SmallInt         |      |                | true        |
	| C18  | SmallMoney       |      |                | true        |
	| C19  | Text             |      |                | true        |
	| C20  | Timestamp        |      |                | true        |
	| C21  | TinyInt          |      |                | true        |
	| C22  | VarBinary        |      |                | true        |
	| C23  | VarChar          |      |                | true        |
	| C25  | Xml              |      |                | true        |
	| C28  | Date             |      |                | true        |
	| C29  | Time             |      |                | true        |
	| C30  | DateTime2        |      |                | true        |
	| C31  | DateTimeOffset   |      |                | true        |

@db
Scenario: Default maximum sizes
	Given there is a test database
	When the table "test" is created
	| Name | Data Type | Size | Decimal Places | Allow Nulls |
	| C13  | NVarChar  | 0    |                | true        |
	| C22  | VarBinary | 0    |                | true        |
	| C23  | VarChar   | 0    |                | true        |
	Then the definition of table "test" should match
	| Name | Data Type | Size | Decimal Places | Allow Nulls |
	| C13  | NVarChar  | 0    |                | true        |
	| C22  | VarBinary | 0    |                | true        |
	| C23  | VarChar   | 0    |                | true        |