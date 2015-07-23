using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests
{
    [Binding]
    public class DatabaseSteps
    {
        private DatabaseActions database;

        [AfterScenario("db")]
        public void AfterScenario()
        {
            if (database == null) return;
            database.Drop();
        }

        [Given(@"there is a test database")]
        public void GivenThereIsATestDatabase()
        {
            database = new DatabaseActions(@"server=(localdb)\v11.0;database=Test2;integrated security=True");
            database.CreateOrReplace();
            ScenarioContext.Current["Database"] = database;
        }

        [When(@"the user '(.*)' is granted access to the database")]
        public void WhenTheUserIsGrantedAccessToTheDatabase(string username)
        {
            database.GrantDomainUserAccess(Environment.UserDomainName, username);
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
                            if (!expected.Contains(reader.GetString(0)))
                                Assert.Fail("Permission not granted: " + reader.GetString(0));
                        }
                }
            }
        }
    }
}
