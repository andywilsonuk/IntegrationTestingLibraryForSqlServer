﻿namespace IntegrationTestingLibraryForSqlServer
{
    public static class ProcedureDefinitionExtensions
    {
        public static void CreateOrReplace(this ProcedureDefinition definition, DatabaseActions database)
        {
            new ProcedureActions(database.ConnectionString).CreateOrReplace(definition);
        }

        public static void VerifyMatch(this ProcedureDefinition definition, DatabaseActions database)
        {
            var check = new ProcedureCheck(database.ConnectionString);
            check.VerifyMatch(definition);
        }
    }
}
