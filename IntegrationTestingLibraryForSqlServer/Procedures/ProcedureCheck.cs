using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ProcedureCheck
    {
        private string connectionString;

        public ProcedureCheck(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void VerifyMatch(ProcedureDefinition expected)
        {
            expected.EnsureValid(false);
            var actual = this.GetDefinition(expected.Name);
            expected.VerifyEqual(actual);
        }

        public ProcedureDefinition GetDefinition(string name)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = name;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlCommandBuilder.DeriveParameters(command);
                    var parameters = command.Parameters.Cast<SqlParameter>().Select(x => ProcedureParameter.FromSqlParameter(x));
                    return new ProcedureDefinition(name, parameters);
                }
            }
        }
    }
}
