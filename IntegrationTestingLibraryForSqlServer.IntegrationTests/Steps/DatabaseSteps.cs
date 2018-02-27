using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests
{
    [Binding]
    public class DatabaseSteps
    {
        private readonly ScenarioContext scenarioContext;
        private DatabaseActions database;
        private string username;

        public DatabaseSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [AfterScenario("db")]
        public void AfterScenario()
        {
            if (database == null) return;
            database.Drop();
        }

        [Given(@"there is a test database")]
        public void GivenThereIsATestDatabase()
        {
            database = new DatabaseActions($@"server=(localdb)\MSSQLLocalDB;database={Guid.NewGuid()};integrated security=True");
            database.CreateOrReplace();
            scenarioContext["Database"] = database;
        }

        [When(@"the domain user '(.*)' is granted access to the database")]
        public void WhenTheDomainUserIsGrantedAccessToTheDatabase(string username)
        {
            DomainAccount account = new DomainAccount(username);
            database.GrantUserAccess(account);
            this.username = account.Qualified;
        }

        [When(@"the SQL user '(.*)' with password '(.*)' is granted access to the database")]
        public void WhenTheSQLUserWithPasswordIsGrantedAccessToTheDatabase(string username, string password)
        {
            database.GrantUserAccess(new SqlAccount(username, password));
            this.username = username;
        }

        [Then(@"the permissions should be")]
        public void ThenThePermissionsShouldBe(Table table)
        {
            string commandFormat = @"EXECUTE AS USER = '{0}';
SELECT permission_name FROM fn_my_permissions (NULL, 'DATABASE');";

            HashSet<string> expected = new HashSet<string>(table.Rows.Select(x => x[0]), StringComparer.CurrentCultureIgnoreCase);

            using (SqlConnection connection = new SqlConnection(database.ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(commandFormat, this.username);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                        while(reader.Read())
                        {
                            Assert.Contains(reader.GetString(0), expected);
                        }
                }
            }
        }
    }
}
