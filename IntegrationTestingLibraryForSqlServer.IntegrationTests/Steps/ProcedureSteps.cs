using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace IntegrationTestingLibraryForSqlServer.IntegrationTests
{
    [Binding]
    public class ProcedureSteps
    {
        private DatabaseActions database = ScenarioContext.Current.Get<DatabaseActions>("Database");

        [Then(@"the definition of procedure ""(.*)"" should match")]
        public void ThenTheDefinitionOfProcedureShouldMatch(string procedureName, Table table)
        {
            ProcedureDefinition definition = new ProcedureDefinition(DatabaseObjectName.FromName(procedureName));
            definition.Parameters.AddFromRaw(table.CreateSet<ProcedureParameterRaw>());
            definition.VerifyMatch(database);
        }

        [Given(@"the procedure ""(.*)"" is created with body ""(.*)""")]
        [When(@"the procedure ""(.*)"" is created with body ""(.*)""")]
        public void GivenTheProcedureIsCreatedWithBody(string procedureName, string body, Table table)
        {
            ProcedureDefinition definition = new ProcedureDefinition(DatabaseObjectName.FromName(procedureName));
            definition.Parameters.AddFromRaw(table.CreateSet<ProcedureParameterRaw>());
            definition.Body = body;
            definition.CreateOrReplace(database);
        }

        private int returnValue;

        [When(@"the procedure ""(.*)"" is executed")]
        public void WhenTheProcedureIsExecuted(string procedureName, Table table)
        {
            using (SqlConnection connection = new SqlConnection(database.ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    var parameters = new List<ProcedureParameterWithValue>(table.CreateSet<ProcedureParameterWithValue>());
                    string bindings = string.Join(",", parameters.Select(x => "@" + x.Name));
                    parameters.Add(new ProcedureParameterWithValue
                    {
                        Name = "@retVal",
                        DataType = "Int",
                        Direction = ParameterDirection.Output
                    });

                    command.Parameters.AddRange(parameters.Select(x => x.ToSqlParameter()).ToArray());
                    command.CommandText = string.Format("exec @retVal = {0} {1}", procedureName, bindings);
                    connection.Open();
                    command.ExecuteNonQuery();
                    returnValue = Convert.ToInt32(command.Parameters["@retVal"].Value);
                }
            }
        }

        [Then(@"the return value should be (.*)")]
        public void ThenTheReturnValueShouldBe(int returnValue)
        {
            Assert.AreEqual(returnValue, this.returnValue);
        }
    }
}
