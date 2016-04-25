using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestingLibraryForSqlServer
{
    public class ProcedureActions
    {
        private string connectionString;

        public ProcedureActions(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(ProcedureDefinition definition)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(new ProcedureCreateSqlGenerator().Sql(definition));
            }
        }

        public void Drop(string name)
        {
            Drop(DatabaseObjectName.FromName(name));
        }

        public void Drop(DatabaseObjectName name)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute(dropProcedureCommand, name);
            }
        }

        public void CreateOrReplace(ProcedureDefinition parameters)
        {
            Drop(parameters.Name);
            Create(parameters);
        }

        private const string dropProcedureCommand = @"if exists (select * from sys.objects where object_id = object_id('{0}') and type = (N'P')) drop procedure {0}";
    }
}
