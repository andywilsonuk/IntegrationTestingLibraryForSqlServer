Feature: Database
	Database setup and tear down functions

@db
Scenario: Grant Domain user access
	Given there is a test database
	When the domain user 'Administrator' is granted access to the database 
	Then the permissions should be
	| Permission                                |
	| CONNECT                                   |
	| SELECT                                    |
	| INSERT                                    |
	| UPDATE                                    |
	| DELETE                                    |
	| EXECUTE                                   |
	| VIEW ANY COLUMN ENCRYPTION KEY DEFINITION |
	| VIEW ANY COLUMN MASTER KEY DEFINITION     |

@db
Scenario: Grant SQL user access
	Given there is a test database
	When the SQL user 'testaccount' with password 'abc123' is granted access to the database 
	Then the permissions should be
	| Permission                                |
	| CONNECT                                   |
	| SELECT                                    |
	| INSERT                                    |
	| UPDATE                                    |
	| DELETE                                    |
	| EXECUTE                                   |
	| VIEW ANY COLUMN ENCRYPTION KEY DEFINITION |
	| VIEW ANY COLUMN MASTER KEY DEFINITION     |