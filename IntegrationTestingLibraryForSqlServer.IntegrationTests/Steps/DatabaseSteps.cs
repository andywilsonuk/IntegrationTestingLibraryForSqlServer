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

        [When(@"the user '(.*)' is granted access to the database")]
        public void WhenTheUserIsGrantedAccessToTheDatabase(string username)
        {
            database.GrantUserAccess(new DomainAccount(username));
        }

        [Then(@"the permissions for '(.*)' should be")]
        public void ThenThePermissionsForShouldBe(string username, Table table)
        {
            string commandFormat = @"EXECUTE AS USER = '{0}\{1}';
SELECT permission_name FROM fn_my_permissions (NULL, 'DATABASE');";

            HashSet<string> expected = new HashSet<string>(table.Rows.Select(x => x[0]), StringComparer.CurrentCultureIgnoreCase);

            using (SqlConnection connection = new SqlConnection(database.ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(commandFormat, Environment.UserDomainName, username);
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
