using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTestingLibraryForSqlServer
{
    public static class ProcedureDefinitionExtensions
    {
        public static void CreateOrReplace(this ProcedureDefinition definition, DatabaseActions database)
        {
            new ProcedureActions(database.ConnectionString).CreateOrReplace(definition);
        }

        public static void VerifyEqual(this ProcedureDefinition definition, DatabaseActions database)
        {
            var check = new ProcedureCheck(database.ConnectionString);
            check.VerifyMatch(definition);
        }
    }
}
