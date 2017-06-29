Feature: Database
	Database setup and tear down functions

@db
Scenario: Grant user access
	Given there is a test database
	When the user 'Administrator' is granted access to the database 
	# change user to be sql auth
	Then the permissions for 'Administrator' should be
	| Permission                                |
	| CONNECT                                   |
	| SELECT                                    |
	| INSERT                                    |
	| UPDATE                                    |
	| DELETE                                    |
	| EXECUTE                                   |
	| VIEW ANY COLUMN ENCRYPTION KEY DEFINITION |
	| VIEW ANY COLUMN MASTER KEY DEFINITION     |
